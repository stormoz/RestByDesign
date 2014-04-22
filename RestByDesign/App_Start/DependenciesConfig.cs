using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;

namespace RestByDesign
{
    public class DependenciesConfig
    {
        public static void Configure()
        {
            var container = IocHelper.CreateContainer(ConfigureMappings);
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        internal static void ConfigureMappings(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

#if (DEBUG)
            builder.RegisterType<DummyUnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.Register(c => new DummyGenericRepository<Client, string>(DummyDataHelper.GetClients()))
                .As<IGenericRepository<Client, string>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<Account, string>(DummyDataHelper.GetAccounts()))
                .As<IGenericRepository<Account, string>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<SmartTag, string>(DummyDataHelper.GetSmartTags()))
                .As<IGenericRepository<SmartTag, string>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<Transaction, string>(DummyDataHelper.GetTransactions()))
                .As<IGenericRepository<Transaction, string>>()
                .SingleInstance();
#else
            builder.RegisterType<RestByDesignContext>()
                .AsSelf()
                .WithParameter("nameOrConnectionString", "name=RestByDesignContext")
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ClientRepository>().As<IGenericRepository<Client, string>>().InstancePerLifetimeScope();
#endif
        }
    }
}