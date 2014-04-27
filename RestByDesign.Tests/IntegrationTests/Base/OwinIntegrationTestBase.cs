using Microsoft.Owin.Testing;
using NUnit.Framework;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Tests.IntegrationTests.Core;

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

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            DummyDataHelper.ResetData();
            Server.Dispose();
        }
    }
}