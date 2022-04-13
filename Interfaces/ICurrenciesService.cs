using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICurrenciesService
    {
        Task<double> Convert(string activeCurrency, string newCurrency);
        List<Currency> GetAvailableCurrencies();
        bool IsSupported(string isoCode);
    }
}