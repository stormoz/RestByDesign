using System.Data.Entity;
using LinqKit;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class RestByDesignContextInitializer 
        : CreateDatabaseIfNotExists<RestByDesignContext>
        //DropCreateDatabaseAlways<RestByDesignContext>
    {
        protected override void Seed(RestByDesignContext context)
        {
            DummyDataHelper.GetList<Client>().ForEach(client => context.Clients.Add(client));
            DummyDataHelper.GetList<Account>().ForEach(account => context.Accounts.Add(account));
            DummyDataHelper.GetList<SmartTag>().ForEach(tag => context.SmartTags.Add(tag));
            DummyDataHelper.GetList<Transaction>().ForEach(transaction => context.Transactions.Add(transaction));

            context.SaveChanges();
        }
    }
}