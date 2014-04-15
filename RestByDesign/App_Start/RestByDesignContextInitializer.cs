using System.Collections.Generic;
using System.Data.Entity;
using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign
{
    public class RestByDesignContextInitializer : DropCreateDatabaseIfModelChanges<RestByDesignContext>
    {
        //DropCreateDatabaseAlways<DBContext>
        //DropCreateDatabaseIfModelChanges<RestByDesignContext>

        protected override void Seed(RestByDesignContext context)
        {
            DummyClients().ForEach(client => context.Clients.Add(client));
            context.SaveChanges();
        }

        private List<Client> DummyClients()
        {
           return new List<Client> { new Client("1", "Foo", new List<Account>()) };

//            IList<Client> _clietns = new[]
//            {
//                new Client
//                {
//                    Id = "1",
//                    Name = "John Doe",
//                    Accounts = new[]
//                    {
//                        new Account
//                        {
//                            Id = "111",
//                            Balance = 0,
//                            Closed = false,
//                            Name = "Savings",
//                            SmartTags = new[]
//                            {
//                                new SmartTag
//                                {
//                                    Id = "1",
//                                    Active = true,
//                                    Deleted = false,
//                                    OrderedDateTime = new DateTime(2014, 02, 1)
//                                },
//                            },
//                            Transactions = new[]
//                            {
//                                new Transaction
//                                {
//                                    Id = "1",
//                                    Amount = 2000,
//                                    EffectDate = new DateTime(2014, 3, 20)
//                                }
//                            }
//                        }
//                    }
//                }
//            }; 
        }
    }
}