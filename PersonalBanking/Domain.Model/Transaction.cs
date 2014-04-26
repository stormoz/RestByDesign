using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Transaction : IEntity
    {
        protected Transaction()
        { }

        public Transaction(string id, decimal amount, DateTime effectDate, string accountId, string description)
        {
            Id = id;
            Amount = amount;
            EffectDate = effectDate;
            AccountId = accountId;
            Description = description;
        }

        public Transaction(decimal amount, DateTime effectDate, string accountId, string description)
        {
            Id = Guid.NewGuid().ToString("N");
            Amount = amount;
            EffectDate = effectDate;
            AccountId = accountId;
            Description = description;
        }

        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime EffectDate { get; private set; }
        public string Description { get; private set; }
        public string AccountId { get; private set; }
    }
}