using Autofac;
using Autofac.Integration.WebApi;
using NUnit.Framework;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Tests.IntegrationTests.Core;

using Microsoft.Owin.Testing; //install-package Microsoft.Owin.Testing

namespace RestByDesign.Tests.IntegrationTests.Base
{
    public class OwinIntegrationTestBase
    {
        public TestServer Server;//http://www.strathweb.com/2013/12/owin-memory-integration-testing/
        
        [TestFixtureSetUp]
        public void FixtureInit()
        {
            Server = TestServer.Create<Startup>();
        }

        [TearDown]
        public void OnAfterEachTest()
        {
            DummyDataHelper.ResetData();
        }
        
        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            Server.Dispose();
        }

        public IUnitOfWork Uow
        {
            get { return Container.Resolve<IUnitOfWork>(); }
        }

        public IContainer Container
        {
            get
            {
                var resolver = Startup.GlobalConfiguration.DependencyResolver as AutofacWebApiDependencyResolver;
                return resolver.Container as IContainer;
            }
        }
    }
}