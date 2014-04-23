using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Services;
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
            ShouldBeAbleToResolve<ITransferService, TransferService>();
#if (DUMMYREPO)
            ShouldBeAbleToResolve<IUnitOfWork, DummyUnitOfWork>();
            ShouldBeAbleToResolve<IGenericRepository<Client>, DummyGenericRepository<Client>>();
            ShouldBeAbleToResolve<IGenericRepository<Account>, DummyGenericRepository<Account>>();
            ShouldBeAbleToResolve<IGenericRepository<SmartTag>, DummyGenericRepository<SmartTag>>();
            ShouldBeAbleToResolve<IGenericRepository<Transaction>, DummyGenericRepository<Transaction>>();
#else
            ShouldBeAbleToResolve<IGenericRepository<Client>, EfGenericRepository<Client>>();
            ShouldBeAbleToResolve<IGenericRepository<Account>, EfGenericRepository<Account>>();
            ShouldBeAbleToResolve<IGenericRepository<SmartTag>, EfGenericRepository<SmartTag>>();
            ShouldBeAbleToResolve<IGenericRepository<Transaction>, EfGenericRepository<Transaction>>();
#endif
        }

        private void ShouldBeAbleToResolve<IT, T>()
        {
            var instance = container.Resolve<IT>();
            instance.ShouldBeOfType<T>();
        }
    }
}
