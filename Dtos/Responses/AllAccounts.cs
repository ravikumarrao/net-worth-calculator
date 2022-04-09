using System;
using System.Collections.Generic;

namespace api.Dtos.Responses
{
    public class AllAccounts
    {
        public List<Account> CashAndInvestments { get; set; }
        public List<Account> LongTermAssets { get; set; }

        public List<Account> ShortTermLiabilities { get; set; }
        public List<Account> LongTermDebt { get; set; }
    }

    public class Account {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
