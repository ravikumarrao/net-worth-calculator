using System.Linq;
using api.Dtos.Responses;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("users/{userId}/net-worth")]
    public class NetWorthCalculatorController : ControllerBase
    {
        private readonly AccountsController _accountsController;
        private readonly UserController _userController;

        public NetWorthCalculatorController(AccountsController accountsController, UserController userController)
        {
            _accountsController = accountsController;
            _userController = userController;
        }

        [HttpGet]
        public CalculateNetworthResponse CalculateNetWorth(int userId)
        {
            var accounts = _accountsController.GetAccounts(userId);

            var balances = accounts.GroupBy(a => a.SubType.GetAccountType())
                .ToDictionary(g => g.Key, g => g.Sum(a => a.Balance));

            return new CalculateNetworthResponse
            {
                TotalAssets = balances[AccountType.Asset],
                TotalLiabilities = balances[AccountType.Liability],
                Currency = _userController.GetUser(userId).Currency
            };
        }
    }
}
