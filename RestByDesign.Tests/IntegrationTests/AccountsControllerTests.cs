using System.Linq;
using System.Net;
using NUnit.Framework;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class AccountsControllerTests : OwinIntegrationTest
    {
        [Test]
        public void Accounts_GetAll_GetByClientId()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts", id);
            var jSend = Server.GetJsendForCollection<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Accounts_GetAll_GetByClientId_WithFieldsFilter()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts?fields=Name", id);
            var jSend = Server.GetJsendForCollection<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.ShouldAllBe(x => x.Id == null);
            jSend.Data.Items.ShouldAllBe(x => x.Balance == null);
            jSend.Data.Items.ShouldAllBe(x => x.Name != null);
        }

        [Test]
        public void Accounts_GetAll_GetByClientIdAndNum()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts/{1}", id, 1);
            var jSend = Server.GetJsendObject<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Accounts_GetAll_GetByClientIdAndNum_WithFields()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts/{1}?fields=Name", id, 1);
            var jSend = Server.GetJsendObject<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(null);
            jSend.Data.Name.ShouldNotBe(null);
            jSend.Data.Balance.ShouldBe(null);
        }

        [Test]
        public void Accounts_GetById()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}", id);
            var jSend = Server.GetJsendObject<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Accounts_GetById_NotFound()
        {
            var id = "333";
            var url = string.Format("/api/accounts/{0}", id);
            var response = Server.GetJsendObject(url);

            response.Status.ShouldBe(JSendStatus.Error);
            response.Code.ShouldBe((int)HttpStatusCode.NotFound);
        }
    }
}