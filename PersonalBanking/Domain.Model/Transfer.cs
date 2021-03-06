using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Transfer : IValueObject
    {
        public Transfer(string accountIdFrom, string accountIdTo, decimal amount, string description)
        {
            if(amount <= 0)
                throw new ArgumentException("Amount to pay should be more than zero.");

            if (string.IsNullOrWhiteSpace(accountIdFrom))
                throw new ArgumentException("AccountIdFrom should be Specified.");

            if (string.IsNullOrWhiteSpace(accountIdTo))
                throw new ArgumentException("AccountIdTo should be Specified.");

            if (amount <= 0)
                throw new ArgumentException("Amount should be greater than zero.");

            if (description != null && description.Length > 100)
                throw new ArgumentException("Description length should not exceed 200 characters.");

            AccountIdFrom = accountIdFrom;
            AccountIdTo = accountIdTo;
            Amount = amount;
            Description = description;
        }

        public string AccountIdFrom { get; private set; }
        public string AccountIdTo { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
    }
}