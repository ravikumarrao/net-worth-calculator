using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace api.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        private static readonly Dictionary<string, Currency> _currencies;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        static CurrencyController()
        {
            _currencies = GetDefaultCurrencies();
            
        }

        public CurrencyController(ILogger<CurrencyController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _apiKey = configuration.GetValue<string>("polygon-io-api-key");
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public List<Currency> GetAvailableCurrencies()
        {
            return _currencies.Values.ToList();
        }

        internal bool IsSupported(string isoCode)
        {
            return _currencies.ContainsKey(isoCode);
        }

        private static Dictionary<string, Currency> GetDefaultCurrencies()
        {
            return new List<Currency>
            {
                new Currency { IsoCode = "USD", Symbol = "$" },
                new Currency { IsoCode = "CAD", Symbol = "$" },
                new Currency { IsoCode = "EUR", Symbol = "€" },
                new Currency { IsoCode = "JPY", Symbol = "¥" },
                new Currency { IsoCode = "GBP", Symbol = "£" },
                new Currency { IsoCode = "AUD", Symbol = "$" },
                new Currency { IsoCode = "NZD", Symbol = "$" },
                new Currency { IsoCode = "THB", Symbol = "฿" },
                new Currency { IsoCode = "INR", Symbol = "₹" },
                new Currency { IsoCode = "LKR", Symbol = "Rs" }
            }
            .ToDictionary(c => c.IsoCode);
        }

        internal async Task<double> Convert(string activeCurrency, string newCurrency)
        {
            var ticker = $"C:{activeCurrency}{newCurrency}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?adjusted=true");
            request.Headers.Add(HeaderNames.Authorization, $"Bearer {_apiKey}");

            var response = await _httpClient.SendAsync(request);

            using (var contentStream = await response.Content.ReadAsStreamAsync())
            {
                var currResponse = await JsonSerializer.DeserializeAsync<PolygonCurrencyPrevCloseResponse>(contentStream);
                return currResponse.Results[0].ClosePrice;
            }
        }
    }
}
