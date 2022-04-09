using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        [HttpPost("calculate-net-worth")]
        public Balances CalculateNetWorth([FromBody] AllAccounts allAccounts)
        {
            return new Balances
            {
                TotalAssets = allAccounts.CashAndInvestments.Sum(a => a.Balance) + allAccounts.LongTermAssets.Sum(a => a.Balance),
                TotalLiabilities = allAccounts.ShortTermLiabilities.Sum(a => a.Balance) + allAccounts.LongTermDebt.Sum(a => a.Balance)
            };
        }
    }
}
