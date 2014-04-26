using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.Core.Helpers;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Services;

namespace RestByDesign
{
    public class DependenciesConfig
    {
        public static void Configure(HttpConfiguration config, bool dummyRepo = false)
        {
            var container = IocHelper.CreateContainer(builder=>ConfigureMappings(builder,dummyRepo));
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }

        internal static void ConfigureMappings(ContainerBuilder builder, bool dummyRepo)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            if (dummyRepo)
            {
                builder.RegisterType<TransferService>().As<ITransferService>().InstancePerLifetimeScope();
                builder.RegisterType<DummyUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

                builder.Register(c => new DummyGenericRepository<Client>(DummyDataHelper.GetList<Client>()))
                    .As<IGenericRepository<Client>>()
                    .InstancePerLifetimeScope();
                builder.Register(c => new DummyGenericRepository<Account>(DummyDataHelper.GetList<Account>()))
                    .As<IGenericRepository<Account>>()
                    .InstancePerLifetimeScope();
                builder.Register(c => new DummyGenericRepository<SmartTag>(DummyDataHelper.GetList<SmartTag>()))
                    .As<IGenericRepository<SmartTag>>()
                    .InstancePerLifetimeScope();
                builder.Register(c => new DummyGenericRepository<Transaction>(DummyDataHelper.GetList<Transaction>()))
                    .As<IGenericRepository<Transaction>>()
                    .InstancePerLifetimeScope();
            }
            else
            {
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
            }
        }
    }
}