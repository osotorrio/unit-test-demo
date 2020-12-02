using System;

namespace InterestRateVigilant
{
    public enum AccountTypes
    {
        Savings,
        Current,
        Simple,
        Join
    }

    public class Account
    {
        public AccountTypes AccountType { get; set; }
        public Guid BrokerId { get; set; }
    }
}