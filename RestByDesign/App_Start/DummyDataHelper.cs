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
            var clients = new[]
            {
                new Client
                {
                    Id = "1",
                    Name = "John Doe",
                    /*Accounts = new[]
                    {
                        new Account
                        {
                            Id = "111",
                            Balance = 1900,
                            Closed = false,
                            Name = "Savings",
                            SmartTags = new[]
                            {
                                new SmartTag
                                {
                                    Id = "1",
                                    Active = true,
                                    Deleted = false,
                                    OrderedDateTime = new DateTime(2014, 02, 1)
                                }
                            },
                            Transactions = new[]
                            {
                                new Transaction
                                {
                                    Id = "1",
                                    Amount = 2000,
                                    EffectDate = new DateTime(2014, 3, 20)
                                },
                                new Transaction
                                {
                                    Id = "2",
                                    Amount = -100,
                                    EffectDate = new DateTime(2014, 3, 21)
                                }
                            }
                        },
                        new Account
                        {
                            Id = "222",
                            Balance = 100,
                            Closed = false,
                            Name = "Cheque",
                            Transactions = new[]
                            {
                                new Transaction
                                {
                                    Id = "1",
                                    Amount = 200,
                                    EffectDate = new DateTime(2014, 4, 14)
                                },
                                new Transaction
                                {
                                    Id = "2",
                                    Amount = -100,
                                    EffectDate = new DateTime(2014, 4, 15)
                                }
                            }
                        }
                    }*/
                }
            };

            return clients.ToList();
        }

        public static List<Account> GetAccounts()
        {
            return new[]
            {
                new Account
                {
                    Id = "111",
                    Balance = 1900,
                    Closed = false,
                    Name = "Savings",
                    ClientId = "1"
                }
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
                new Transaction
                {
                    Id = "1",
                    Amount = 2000,
                    EffectDate = new DateTime(2014, 3, 20),
                    AccountId = "111"
                },
                new Transaction
                {
                    Id = "2",
                    Amount = -100,
                    EffectDate = new DateTime(2014, 3, 21),
                    AccountId = "111"
                }
            }.ToList();
        }
    }
}