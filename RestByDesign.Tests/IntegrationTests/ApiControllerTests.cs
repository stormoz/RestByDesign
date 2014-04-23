using System.Linq;
using System.Net;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class ApiControllerTests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public void GetById()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);
            var jSend = _server.GetJsendObject<ClientModel>(url);
            
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(clientId);
        }

        [Test]
        public void GetAccounts()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}/accounts", clientId);
            var jSend = _server.GetJsendForCollection<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(2);
        }

        [Test]
        public void GetAccountById()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}", id);
            var response = _server.GetResponse(url);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
