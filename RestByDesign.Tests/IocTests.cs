using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models;
using Shouldly;

namespace RestByDesign.Tests
{
    [TestClass]
    public class IocTests
    {
        private readonly IContainer container;

        public IocTests()
        {
            container = IocHelper.CreateContainer();
        }

        [TestMethod]
        public void TestIocConfiguration()
        {
            ShouldBeAbleToResolve<IUnitOfWork, EntityFrameworkUnitOfWork>();
            ShouldBeAbleToResolve<RestByDesignContext, RestByDesignContext>();
            ShouldBeAbleToResolve<IGenericRepository<Client, string>, ClientRepository>();
        }

        private void ShouldBeAbleToResolve<IT, T>()
        {
            var instance = container.Resolve<IT>();
            instance.ShouldBeOfType<T>();
        }
    }
}
