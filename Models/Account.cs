using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public AccountSubType SubType { get; set; }
        public AccountType Type { get => SubType.GetAccountType(); }

        public Account Clone()
        {
            return new Account
            {
                Id = Id,
                Name = Name,
                Balance = Balance,
                SubType = SubType
            };
        }
    }

    public enum AccountSubType
    {
        CashAndInvestments,
        LongTermAssets,

        ShortTermLiabilities,
        LongTermDebt
    }

    public enum AccountType
    {
        Asset,
        Liability
    }

    public static class AccountTypeEx
    {

        public static AccountType GetAccountType(this AccountSubType subType)
        {
            switch (subType)
            {
                case AccountSubType.CashAndInvestments:
                case AccountSubType.LongTermAssets:
                    return AccountType.Asset;
                case AccountSubType.ShortTermLiabilities:
                case AccountSubType.LongTermDebt:
                    return AccountType.Liability;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
