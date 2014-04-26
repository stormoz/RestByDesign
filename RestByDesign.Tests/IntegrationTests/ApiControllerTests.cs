using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Core;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class ApiControllerTests
    {
        private TestServer _server;//http://www.strathweb.com/2013/12/owin-memory-integration-testing/

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            DummyDataHelper.ResetData();
            _server.Dispose();
        }

        [Test]
        public void Clients_GetById()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);
            var jSend = _server.GetJsendObject<ClientModel>(url);
            
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(clientId);
        }

        [Test]
        public void Clients_GetById_WithFieldsFilter()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}?fields=Name", clientId);
            var jSend = _server.GetJsendObject<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Clients_GetAll()
        {
            var url = "/api/clients";
            var jSend = _server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Clients_GetAll_WithPaging()
        {
            var url = "/api/clients?skip=0,take=1";
            var jSend = _server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
        }

        [Test]
        public void Clients_GetAll_WithFieldFilter()
        {
            var url = "/api/clients?fields=Name";
            var jSend = _server.GetJsendForCollection<ClientModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.ShouldAllBe(x => x.Id == null);
            jSend.Data.Items.ShouldAllBe(x => x.Name != null);
        }

        [Test]
        public void Accounts_GetAll_GetByClientId()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts", id);
            var jSend = _server.GetJsendForCollection<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Accounts_GetAll_GetByClientId_WithFieldsFilter()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts?fields=Name", id);
            var jSend = _server.GetJsendForCollection<AccountModel>(url);

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
            var jSend = _server.GetJsendObject<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Accounts_GetAll_GetByClientIdAndNum_WithFields()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/accounts/{1}?fields=Name", id, 1);
            var jSend = _server.GetJsendObject<AccountModel>(url);

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
            var jSend = _server.GetJsendObject<AccountModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Name.ShouldNotBe(null);
        }

        [Test]
        public void Accounts_GetById_NotFound()
        {
            var id = "333";
            var url = string.Format("/api/accounts/{0}", id);
            var response = _server.GetJsendObject(url);

            response.Status.ShouldBe(JSendStatus.Error);
            response.Code.ShouldBe((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountId()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags", id);
            var jSend = _server.GetJsendForCollection<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountId_WithFieldsFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags?fields=Active", id);
            var jSend = _server.GetJsendForCollection<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.ShouldAllBe(x => x.Id == null);
            jSend.Data.Items.ShouldAllBe(x => x.Version == null);
            jSend.Data.Items.ShouldAllBe(x => x.Active != null);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountIdAndNum()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags/{1}", id, 1);
            var jSend = _server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountIdAndNum_WithFields()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags/{1}?fields=Active", id, 1);
            var jSend = _server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(null);
            jSend.Data.Active.ShouldNotBe(null);
            jSend.Data.Version.ShouldBe(null);
        }

        [Test]
        public void SmartTags_GetById()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);
            var jSend = _server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void SmartTags_GetById_NotFound()
        {
            var id = "3";
            var url = string.Format("/api/smarttags/{0}", id);
            var response = _server.GetJsendObject(url);

            response.Status.ShouldBe(JSendStatus.Error);
            response.Code.ShouldBe((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void SmartTags_Patch()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);
            var jSend = _server.GetJsendObject<SmartTagModel>(url);
            var currentValue = jSend.Data.Active;

            var updatedjSend = _server.GetJsendObject<SmartTagModel>(url, HttpVerbs.Patch, new { active = !currentValue });
            updatedjSend.Status.ShouldBe(JSendStatus.Success);
            updatedjSend.Data.Id.ShouldNotBe(null);
            updatedjSend.Data.Active.ShouldBe(!currentValue);
        }

        [Test]
        public void SmartTags_Patch_WithInvalidModel()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);

            var updatedjSend = _server.GetJsendObject(url, HttpVerbs.Patch, new { active = (bool?)null });
            updatedjSend.Status.ShouldBe(JSendStatus.Fail);

            var jSend = _server.GetJsendObject<SmartTagModel>(url);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void Transactions_GetAllByAccountId()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions", id);

            var jSend = _server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Transactions_GetAllByAccountId_WithFieldsFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions?fields=Amount", id);

            var jSend = _server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);

            var transactions = jSend.Data.Items.ToList();
            transactions.Count().ShouldBeGreaterThan(0);
            transactions.ShouldAllBe(x => x.Amount != null);
            transactions.ShouldAllBe(x => x.Id == null);
            transactions.ShouldAllBe(x => x.EffectDate == null);
            transactions.ShouldAllBe(x => x.Description == null);
        }

        [Test]
        public void Transactions_GetAllByAccountId_Paging()
        {
            var id = "111"; 
            var url = string.Format("/api/accounts/{0}/transactions?skip=0&take=1", id);

            var jSend = _server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
        }

        [Test]
        public void Transactions_GetAllByAccountId_TransactionFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions?dateFrom=2014-04-01&amountFrom=100", id);

            var jSend = _server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
        }

        [Test]
        public void Transfers_Post()
        {
            DummyDataHelper.GetList<Transaction>().Count.ShouldBe(2);

            var id = "1";
            var url = string.Format("/api/clients/{0}/transfers", id);

            var jSend = _server.GetJsendObject(url, HttpVerbs.Post, new { accountFrom = "111", accountTo = "222", amount = 1000, description = "car" });
            jSend.Status.ShouldBe(JSendStatus.Success);

            DummyDataHelper.GetList<Transaction>().Count.ShouldBe(4);
        }

        [Test]
        public void Transfers_PostToBigAmount_ShouldFail()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/transfers", id);
            
            var jSend = _server.GetJsendObject(url, HttpVerbs.Post, new {accountFrom = "111", accountTo="222", amount=11000, description="car"});
            jSend.Status.ShouldBe(JSendStatus.Fail);
        }
    }
}
