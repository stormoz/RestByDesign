using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Transaction : IEntity<string>
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectDate { get; set; }
        public string AccountId { get; set; }
    }
}