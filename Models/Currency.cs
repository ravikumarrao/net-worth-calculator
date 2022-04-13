using System;
namespace api.Models
{
    public class Currency
    {
        public string IsoCode { get; set; }
        public string Symbol { get; set; }
        public string Locale { get; set; } = "en-US";
    }
}
