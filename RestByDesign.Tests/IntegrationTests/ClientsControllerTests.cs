using System.Linq;
using NUnit.Framework;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class ClientsControllerTests : OwinIntegrationTest
    {
        [Test]
        public void Clients_GetById()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);
            var jSend = Server.GetJsendObject<ClientModel>(url);
            
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(clientId);
        }

        [Test]
        public void Clients_GetById_WithFieldsFilter()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}?fields=Name", clientId);
            var jSend = Server.GetJsendObject<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Clients_GetAll()
        {
            var url = "/api/clients";
            var jSend = Server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Clients_GetAll_WithPaging()
        {
            var url = "/api/clients?skip=0,take=1";
            var jSend = Server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
        }

        [Test]
        public void Clients_GetAll_WithFieldFilter()
        {
            var url = "/api/clients?fields=Name";
            var jSend = Server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.ShouldAllBe(x => x.Id == null);
            jSend.Data.Items.ShouldAllBe(x => x.Name != null);
        }
    }
}
