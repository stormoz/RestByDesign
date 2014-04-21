using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using Shouldly;

namespace RestByDesign.Tests
{
    [TestClass]
    public class IocTests
    {
        private readonly IContainer container;

        public IocTests()
        {
            container = IocHelper.CreateContainer(DependenciesConfig.ConfigureMappings);
        }

        [TestMethod]
        public void TestIocConfiguration()
        {
#if DEBUG
            ShouldBeAbleToResolve<IUnitOfWork, DummyUnitOfWork>();
            ShouldBeAbleToResolve<IGenericRepository<Client, string>, DummyGenericRepository<Client, string>>();
#else
            ShouldBeAbleToResolve<IUnitOfWork, EntityFrameworkUnitOfWork>();
            ShouldBeAbleToResolve<RestByDesignContext, RestByDesignContext>();
            ShouldBeAbleToResolve<IGenericRepository<Client, string>, ClientRepository>();
#endif
        }

        private void ShouldBeAbleToResolve<IT, T>()
        {
            var instance = container.Resolve<IT>();
            instance.ShouldBeOfType<T>();
        }
    }
}
