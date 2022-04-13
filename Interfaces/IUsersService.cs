using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Requests;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IUsersService
    {
        List<User> GetUser();
        User GetUser(int id);
        Task<IActionResult> UpdateUser(int id, [FromBody] PatchUserRequest request);
    }
}