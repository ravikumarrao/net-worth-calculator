using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Requests;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IAccountsService
    {
        IActionResult GetAccount(int userId, int accountId);
        List<Account> GetAccounts(int userId);
        IActionResult UpdateAccount(int userId, int accountId, [FromBody] PatchAccountRequest request);
        Task UpdateCurrency(int userId, string currentCurrency, string newCurrency);
    }
}