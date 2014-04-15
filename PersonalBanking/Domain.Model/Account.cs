

using System.Collections.Generic;

namespace PersonalBanking.Domain.Model
{
    public class Account
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
        public bool Closed { get; set; }
        public IList<SmartTag> SmartTags { get; set; }
        public IList<Transaction> Transactions { get; set; } 
    }
}