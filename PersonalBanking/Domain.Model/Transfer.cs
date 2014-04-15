using System;

namespace PersonalBanking.Domain.Model
{
    public class Transfer
    {
        public Transfer()
        {
            
        }

        public Transfer(string accountIdFrom, string accountIdTo, decimal amount, string description)
        {
            if(amount <= 0)
                throw new ArgumentException("Amount to pay should be more than zero.");

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