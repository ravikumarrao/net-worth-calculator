using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Models
{

    public class Result
    {
        [JsonPropertyName("c")]
        public double ClosePrice { get; set; }
    }

    public class PolygonCurrencyPrevCloseResponse
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }
}
