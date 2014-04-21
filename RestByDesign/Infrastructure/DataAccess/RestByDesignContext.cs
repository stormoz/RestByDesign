using System.Data.Entity;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class RestByDesignContext : DbContext
    {       
        public RestByDesignContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            
        }

        public DbSet<Client> Clients { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}
