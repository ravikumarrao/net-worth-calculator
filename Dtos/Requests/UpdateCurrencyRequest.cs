using System;
using System.Collections.Generic;
using api.Dtos.Responses;
using api.Models;

namespace api.Dtos.Requests
{
    public class UpdateCurrencyRequest
    {
        public string ActiveCurrency { get; set; }
        public string NewCurrency { get; set; }
        public List<Account> AllAccounts { get; set; }
    }
}
