using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("metadata")]
    public class MetadataController : Controller
    {
        private readonly ILogger<MetadataController> _logger;

        public MetadataController(ILogger<MetadataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all-accounts")]
        public AllAccounts allAccounts() {
            return new AllAccounts {
                CashAndInvestments = new List<Account>
                {
                    new Account { Name = "Chequing", Balance = 2000 },
                    new Account { Name = "Savings for Taxes", Balance = 4000 },
                    new Account { Name = "Rainy day Fund", Balance = 506 },
                    new Account { Name = "Savings for Fun", Balance = 5000 },
                    new Account { Name = "Savings for Travel", Balance = 400 },
                    new Account { Name = "Savings for Personal Development", Balance = 200 },
                    new Account { Name = "Investment 1", Balance = 5000 },
                    new Account { Name = "Investment 2", Balance = 60000 },
                    new Account { Name = "Investment 3", Balance = 24000 },
                },
                LongTermAssets = new List<Account>
                {
                    new Account { Name = "Primary Home", Balance = 455000 },
                    new Account { Name = "Secondary Home", Balance = 1564321 },
                    new Account { Name = "Other" },
                },
                ShortTermLiabilities = new List<Account>
                {
                    new Account { Name = "Credit Card 1", Balance = 4342 },
                    new Account { Name = "Credit Card 2", Balance = 322 },
                },
                LongTermDebt = new List<Account>
                {
                    new Account { Name = "Mortgage 1", Balance = 250999 },
                    new Account { Name = "Mortgage 2", Balance = 632634 },
                    new Account { Name = "Line of Credit", Balance = 10000 },
                    new Account { Name = "Investment Loan", Balance = 10000 },
                }
            };
        }
    }
}
