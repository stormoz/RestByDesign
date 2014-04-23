using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Account : IEntity
    {
        public Account()
        {
            
        }

        public Account(string id, string name, decimal balance, bool closed, string clientId)
        {
            Id = id;
            Name = name;
            Balance = balance;
            Closed = closed;
            ClientId = clientId;
        }
   
        public string Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public string ClientId { get; private set; }
        public bool Closed { get; private set; }

        public void TakeFromAccount(decimal amount)
        {
            Balance -= amount;
        }

        public void PutToAccount(decimal amount)
        {
            Balance += amount;
        }
    }
}