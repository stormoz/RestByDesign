using System.Data.Entity;
using RestByDesign.Infrastructure.DataAccess;

namespace RestByDesign
{
    public class RestByDesignContextInitializer 
        : DropCreateDatabaseAlways<RestByDesignContext>
        //: DropCreateDatabaseIfModelChanges<RestByDesignContext>
    {
        protected override void Seed(RestByDesignContext context)
        {
            DummyDataHelper.GetClients().ForEach(client => context.Clients.Add(client));
            DummyDataHelper.GetAccounts().ForEach(account => context.Accounts.Add(account));
            DummyDataHelper.GetSmartTags().ForEach(tag => context.SmartTags.Add(tag));
            DummyDataHelper.GetTransactions().ForEach(transaction => context.Transactions.Add(transaction));

            context.SaveChanges();
        }
    }
}