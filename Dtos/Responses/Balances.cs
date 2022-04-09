using System;
namespace api.Dtos.Responses
{
    public class Balances
    {
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal NetWorth => TotalAssets - TotalLiabilities;
    }
}
