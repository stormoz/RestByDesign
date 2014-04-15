using System;

namespace PersonalBanking.Domain.Model
{
    public class Transaction
    {
        public Transaction()
        {
            
        }

        public Transaction(string id, decimal amount, DateTime effecDate)
        {
            Id = id;
            Amount = amount;
            EffectDate = effecDate;
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectDate { get; set; }
    }
}