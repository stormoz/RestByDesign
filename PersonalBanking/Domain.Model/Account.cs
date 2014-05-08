using System;
using PersonalBanking.Domain.Model.Core;
using PersonalBanking.Domain.Model.Exceptions;

namespace PersonalBanking.Domain.Model
{
    public class Account : IEntity
    {
        protected Account()
        { }

        public Account(string id, string name, decimal balance, bool closed)
        {
            Id = id;
            Name = name;
            Balance = balance;
            Closed = closed;
        }
   
        public string Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public bool Closed { get; private set; }

        public virtual Client Client { get; set; }


        public void TakeFromAccount(decimal amount)
        {
            if(amount <= 0)
                throw new ArgumentException("Amount to take from account should be greater than 0");

            if (amount > Balance)
                throw new PersonalBankingException("Not enough money to take amount from account");

            Balance -= amount;
        }

        public void PutToAccount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount to take from account should be greater than 0");

            Balance += amount;
        }
    }
}