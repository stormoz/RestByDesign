using System;
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
            context.SaveChanges();
        }
    }
}