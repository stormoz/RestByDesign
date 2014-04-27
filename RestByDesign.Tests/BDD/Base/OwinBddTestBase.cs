using Microsoft.Owin.Testing;
using NUnit.Framework;
using RestByDesign.Tests.IntegrationTests.Core;

namespace RestByDesign.Tests.BDD.Base
{
    public class OwinBddTestBase
    {
        public TestServer Server;

        public OwinBddTestBase()
        {
            Server = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            Server.Dispose();
        }
    }
}
