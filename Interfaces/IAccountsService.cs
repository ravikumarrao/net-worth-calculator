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
        Task<List<Account>> GetAccounts(int userId, string currency);
        IActionResult UpdateAccount(int userId, int accountId, [FromBody] PatchAccountRequest request);
        Task UpdateCurrency(int userId, string currentCurrency, string newCurrency);
    }
}