using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class RestByDesignContext : DbContext
    {       
        public RestByDesignContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SmartTag> SmartTags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
