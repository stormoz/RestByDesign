using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Services;

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


#if (DUMMYREPO)
            builder.RegisterType<TransferService>().As<ITransferService>().SingleInstance();
            builder.RegisterType<DummyUnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.Register(c => new DummyGenericRepository<Client>(DummyDataHelper.GetClients()))
                .As<IGenericRepository<Client>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<Account>(DummyDataHelper.GetAccounts()))
                .As<IGenericRepository<Account>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<SmartTag>(DummyDataHelper.GetSmartTags()))
                .As<IGenericRepository<SmartTag>>()
                .SingleInstance();
            builder.Register(c => new DummyGenericRepository<Transaction>(DummyDataHelper.GetTransactions()))
                .As<IGenericRepository<Transaction>>()
                .SingleInstance();
#else
            builder.RegisterType<TransferService>().As<ITransferService>().InstancePerLifetimeScope();
            builder.RegisterType<RestByDesignContext>()
                .AsSelf()
                .WithParameter("nameOrConnectionString", "name=RestByDesignContext")
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<EfGenericRepository<Client>>().As<IGenericRepository<Client>>().InstancePerLifetimeScope();
            builder.RegisterType<EfGenericRepository<Account>>().As<IGenericRepository<Account>>().InstancePerLifetimeScope();
            builder.RegisterType<EfGenericRepository<SmartTag>>().As<IGenericRepository<SmartTag>>().InstancePerLifetimeScope();
            builder.RegisterType<EfGenericRepository<Transaction>>().As<IGenericRepository<Transaction>>().InstancePerLifetimeScope();
#endif
        }
    }
}