using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Responses;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("users/{userId}/net-worth")]
    public class CalculatorController : ControllerBase, ICalculatorService
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;

        public CalculatorController(IAccountsService accountsService, IUsersService usersService)
        {
            _accountsService = accountsService;
            _userService = usersService;
        }

        [HttpGet]
        public async Task<CalculateNetworthResponse> CalculateNetWorth(int userId, [FromQuery(Name = "currency")] string currency)
        {
            var accounts = await _accountsService.GetAccounts(userId, currency);

            var balances = accounts.GroupBy(a => a.SubType.GetAccountType())
                .ToDictionary(g => g.Key, g => g.Sum(a => a.Balance));

            return new CalculateNetworthResponse
            {
                TotalAssets = balances[AccountType.Asset],
                TotalLiabilities = balances[AccountType.Liability],
                Currency = _userService.GetUser(userId).Currency
            };
        }
    }
}
