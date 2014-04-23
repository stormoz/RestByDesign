using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Transaction : IEntity
    {
        public Transaction()
        {
            
        }

        public Transaction(string id, decimal amount, DateTime effectDate, string accountId)
        {
            Id = id;
            Amount = amount;
            EffectDate = effectDate;
            AccountId = accountId;
        }

        public Transaction(decimal amount, DateTime effectDate, string accountId)
        {
            Id = Guid.NewGuid().ToString("N");
            Amount = amount;
            EffectDate = effectDate;
            AccountId = accountId;
        }

        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime EffectDate { get; private set; }
        public string AccountId { get; private set; }
    }
}