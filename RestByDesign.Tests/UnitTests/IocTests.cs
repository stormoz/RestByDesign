using Autofac;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core.Helpers;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Services;
using Shouldly;

namespace RestByDesign.Tests.UnitTests
{
    public class IocTests
    {
        private IContainer container;

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestIocConfiguration(bool dummyRepo)
        {
            container = IocHelper.CreateContainer(builder => DependenciesConfig.ConfigureMappings(builder, dummyRepo));

            ShouldBeAbleToResolve<ITransferService, TransferService>();
            if (dummyRepo)
            {

                ShouldBeAbleToResolve<IUnitOfWork, DummyUnitOfWork>();
                ShouldBeAbleToResolve<IGenericRepository<Client>, DummyGenericRepository<Client>>();
                ShouldBeAbleToResolve<IGenericRepository<Account>, DummyGenericRepository<Account>>();
                ShouldBeAbleToResolve<IGenericRepository<SmartTag>, DummyGenericRepository<SmartTag>>();
                ShouldBeAbleToResolve<IGenericRepository<Transaction>, DummyGenericRepository<Transaction>>();
            }
            else
            {
                ShouldBeAbleToResolve<IGenericRepository<Client>, EfGenericRepository<Client>>();
                ShouldBeAbleToResolve<IGenericRepository<Account>, EfGenericRepository<Account>>();
                ShouldBeAbleToResolve<IGenericRepository<SmartTag>, EfGenericRepository<SmartTag>>();
                ShouldBeAbleToResolve<IGenericRepository<Transaction>, EfGenericRepository<Transaction>>();
            }
        }

        private void ShouldBeAbleToResolve<IT, T>()
        {
            var instance = container.Resolve<IT>();
            instance.ShouldBeOfType<T>();
        }
    }
}
