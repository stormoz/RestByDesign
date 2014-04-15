using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PersonalBanking.Domain.Model;

namespace RestByDesign.Models
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
