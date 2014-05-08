using System.Collections.Generic;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Client : IEntity
    {
        protected Client()
        { }

        public Client(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Client(string id, string name, ICollection<Account> accounts)
        {
            Id = id;
            Name = name;
            Accounts = accounts;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}