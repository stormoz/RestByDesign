using System.Collections.Generic;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Account : IEntity<string>
    {
        public Account()
        {
            
        }

        public Account(string id, string name, decimal balance, bool closed)
        {
            Id = id;
            Name = name;
            Balance = balance;
            Closed = closed;
        }
   
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string ClientId { get; set; }
        public bool Closed { get; set; }
        public IList<SmartTag> SmartTags { get; set; }
        public IList<Transaction> Transactions { get; set; } 
    }
}