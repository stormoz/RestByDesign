using System;
using System.Collections.Generic;
using System.Linq;
using PersonalBanking.Domain.Model;

namespace RestByDesign
{
    public static class DummyDataHelper
    {
        public static List<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    Id = "1",
                    Name = "John Doe"
                }
            }.ToList();
        }

        public static List<Account> GetAccounts()
        {
            return new[]
            {
                new Account("111","Savings",10000,false,"1"),
                new Account("222","Cheque",100,false,"1")
            }.ToList();
        }

        public static List<SmartTag> GetSmartTags()
        {
            return new[]
            {
                new SmartTag
                {
                    Id = "1",
                    AccountId = "111",
                    Active = true,
                    Deleted = false,
                    OrderedDateTime = new DateTime(2014, 02, 1)
                }
            }.ToList();
        }

        public static List<Transaction> GetTransactions()
        {
            return new[]
            {
                new Transaction("1",2000,new DateTime(2014, 3, 20),"111"),
                new Transaction("2",-100,new DateTime(2014, 3, 21),"111")
            }.ToList();
        }
    }
}