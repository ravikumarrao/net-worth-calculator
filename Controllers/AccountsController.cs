using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Dtos.Requests;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("users/{userId}/acccounts")]
    public class AccountsController : ControllerBase
    {
        private static int _NEXT_ACCOUNT_ID = 1;
        private static readonly Dictionary<int, Dictionary<int, Account>> _userAccounts = new Dictionary<int, Dictionary<int, Account>>();

        private readonly ILogger<AccountsController> _logger;
        private readonly CurrencyController _currencyController;

        public AccountsController(ILogger<AccountsController> logger, CurrencyController currencyController)
        {
            _logger = logger;
            _currencyController = currencyController;
        }

        [HttpGet]
        public List<Account> GetAccounts(int userId)
        {
            if (!_userAccounts.ContainsKey(userId))
            {
                initDummyAccounts(userId);
            }

            return _userAccounts[userId].Values.ToList();
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccount(int userId, int accountId)
        {
            if (!_userAccounts.ContainsKey(userId))
            {
                initDummyAccounts(userId);
            }

            if (!_userAccounts[userId].ContainsKey(accountId))
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(_userAccounts[userId][accountId]);
        }

        [HttpPatch("{accountId}")]
        public IActionResult UpdateAccount(int userId, int accountId, [FromBody] PatchAccountRequest request)
        {
            if (!_userAccounts.ContainsKey(userId))
            {
                initDummyAccounts(userId);
            }

            if (!_userAccounts[userId].ContainsKey(accountId))
            {
                return new NotFoundResult();
            }

            _userAccounts[userId][accountId].Balance = request.Balance;

            return new OkResult();
        }

        internal async Task UpdateCurrency(int userId, string currentCurrency, string newCurrency)
        {
            if (currentCurrency == newCurrency) return;

            var factor = await _currencyController.Convert(currentCurrency, newCurrency);

            _userAccounts[userId].Values.ToList().ForEach(a => a.Balance *= factor);
        }

        private void initDummyAccounts(int userId)
        {
            var accounts = new List<Account> {
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Chequing", Balance = 2000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Savings for Taxes", Balance = 4000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Rainy day Fund", Balance = 506 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Savings for Fun", Balance = 5000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Savings for Travel", Balance = 400 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Savings for Personal Development", Balance = 200 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Investment 1", Balance = 5000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Investment 2", Balance = 60000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.CashAndInvestments, Name = "Investment 3", Balance = 24000 },

                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermAssets, Name = "Primary Home", Balance = 455000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermAssets, Name = "Secondary Home", Balance = 1564321 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermAssets, Name = "Other" },

                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.ShortTermLiabilities, Name = "Credit Card 1", Balance = 4342 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.ShortTermLiabilities, Name = "Credit Card 2", Balance = 322 },

                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermDebt, Name = "Mortgage 1", Balance = 250999 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermDebt, Name = "Mortgage 2", Balance = 632634 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermDebt, Name = "Line of Credit", Balance = 10000 },
                new Account { Id = Interlocked.Increment(ref _NEXT_ACCOUNT_ID), SubType = AccountSubType.LongTermDebt, Name = "Investment Loan", Balance = 10000 }
            };

            _userAccounts[userId] = accounts.ToDictionary(a => a.Id);
        }
    }
}
