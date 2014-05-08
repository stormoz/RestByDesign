using System;
using System.Collections.Generic;
using System.Linq;
using PersonalBanking.Domain.Model;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public static class DummyDataHelper
    {
        private static IList<Client> Clients;
        private static IList<Account> Accounts;
        private static IList<SmartTag> SmartTags;
        private static IList<Transaction> Transactions;

        static DummyDataHelper()
        {
            InitData();
        }

        public static void ResetData()
        {
            InitData();
        }

        public static IList<T> GetList<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Client))
                return (IList<T>)Clients;

            if (typeof(T) == typeof(Account))
                return (IList<T>)Accounts;

            if (typeof(T) == typeof(SmartTag))
                return (IList<T>)SmartTags;

            if (typeof(T) == typeof(Transaction))
                return (IList<T>)Transactions;

            throw new ArgumentOutOfRangeException("No data for type found");
        }

        private static void InitData()
        {
            Clients = GetClients();

            Accounts = GetAccounts();

            foreach (var account in Accounts)
            {
                account.Client = Clients[0];
            }

            Clients[0].Accounts = Accounts;

            SmartTags = GetSmartTags();
            Transactions = GetTransactions();
        }

        public static IList<Client> GetClients()
        {
            var client = new Client("1", "John Doe");
            

            return new[] { client }.ToList();
        }

        public static IList<Account> GetAccounts()
        {
            return new[]
            {
                new Account("111","Savings",10000,false),
                new Account("222","Cheque",100,false)
            }.ToList();
        }

        public static IList<SmartTag> GetSmartTags()
        {
            return new[]
            {
                new SmartTag("1", "111", true, false, 1, new DateTime(2014, 02, 1))
            }.ToList();
        }

        public static List<Transaction> GetTransactions()
        {
            return new[]
            {
                new Transaction("1",2000,new DateTime(2014, 3, 20),"111","Insurance"),
                new Transaction("2",-100,new DateTime(2014, 4, 20),"111","Car")
            }.ToList();
        }
    }
}