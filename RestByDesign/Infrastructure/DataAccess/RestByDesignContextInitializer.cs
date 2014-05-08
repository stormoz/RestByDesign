using System.Data.Entity;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class RestByDesignContextInitializer : DropCreateDatabaseIfModelChanges<RestByDesignContext>
    {
        protected override void Seed(RestByDesignContext context)
        {
            context.Clients.AddRange(DummyDataHelper.GetList<Client>());
            context.SmartTags.AddRange(DummyDataHelper.GetList<SmartTag>());
            context.Transactions.AddRange(DummyDataHelper.GetList<Transaction>());

            context.SaveChanges();
        }
    }
}