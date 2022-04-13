using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Dtos.Requests;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase, IUsersService
    {
        private static int DUMMY_USER_ID = 42;

        private readonly ILogger<UsersController> _logger;
        private static Dictionary<int, User> _users = new Dictionary<int, User>();
        private ICurrenciesService _currencyService;


        public UsersController(ILogger<UsersController> logger, ICurrenciesService currenciesService)
        {
            _logger = logger;
            _currencyService = currenciesService;
        }

        [HttpGet]
        public List<User> GetUser()
        {
            if (!_users.ContainsKey(DUMMY_USER_ID))
            {
                InitDummyUser();
            }
            return _users.Values.ToList();
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            id = DUMMY_USER_ID;
            if (!_users.ContainsKey(id))
            {
                InitDummyUser();
            }
            return _users[id];
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] PatchUserRequest request)
        {
            if (!_users.ContainsKey(id))
            {
                return new NotFoundResult();
            }
            if (!_currencyService.IsSupported(request.Currency))
            {
                return new BadRequestResult();
            }

            var prevCurrency = _users[id].Currency;
            _users[id].Currency = request.Currency;
            //await _accountsService.UpdateCurrency(id, prevCurrency, request.Currency);
            return new OkResult();
        }

        private void InitDummyUser()
        {
            var id = DUMMY_USER_ID;
            _users[id] = new User
            {
                Id = id,
                Name = $"Dummy User {id}",
                Currency = "USD"
            };
        }
    }
}
