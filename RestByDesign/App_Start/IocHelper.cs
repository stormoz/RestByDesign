using System;
using Antlr.Runtime.Misc;
using Autofac;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models;

namespace RestByDesign
{
    public static class IocHelper
    {
        public static IContainer CreateContainer(Action<ContainerBuilder> mapping = null)
        {
            var builder = new ContainerBuilder();
            RegisterMapping(builder, mapping);
            return builder.Build(); 
        }

        private static void RegisterMapping(ContainerBuilder builder, Action<ContainerBuilder> mapping)
        {
            if (mapping != null)
                mapping(builder);

            builder.RegisterType<RestByDesignContext>()
                .AsSelf()
                .WithParameter("nameOrConnectionString", "name=RestByDesignContext")
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ClientRepository>().As<IGenericRepository<Client,string>>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(SaveTransaction).Assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof (IHandle<>)))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
        }
    }
}