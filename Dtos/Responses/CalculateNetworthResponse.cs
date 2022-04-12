using System;
namespace api.Dtos.Responses
{
    public class CalculateNetworthResponse
    {
        public double TotalAssets { get; set; }
        public double TotalLiabilities { get; set; }
        public double NetWorth => TotalAssets - TotalLiabilities;
        public string Currency { get; set; }
    }
}
