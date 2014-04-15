using System.Collections.Generic;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Client : IEntity<string>
    {
        public Client()
        {
            
        }

        public Client(string id, string name, IList<Account> accounts)
        {
            Id = id;
            Name = name;
            Accounts = accounts;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IList<Account> Accounts { get; set; }
    }
}