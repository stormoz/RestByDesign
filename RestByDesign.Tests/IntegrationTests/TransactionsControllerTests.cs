using System.Linq;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class TransactionsControllerTests : OwinIntegrationTestBase
    {
        [Test]
        public void Transactions_GetAllByAccountId()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions", id);

            var jSend = Server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void Transactions_GetAllByAccountId_Paged()
        {
            var totalTransactions = DummyDataHelper.GetList<Transaction>().Count;
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions?skip=0&take=1", id);

            var jSend = Server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
            jSend.Data.Count.ShouldBe(totalTransactions);
        }

        [Test]
        public void Transactions_GetAllByAccountId_WithFieldsFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions?fields=Amount", id);

            var jSend = Server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);

            var transactions = jSend.Data.Items.ToList();
            transactions.Count().ShouldBeGreaterThan(0);
            transactions.ShouldAllBe(x => x.Amount != null);
            transactions.ShouldAllBe(x => x.Id == null);
            transactions.ShouldAllBe(x => x.EffectDate == null);
            transactions.ShouldAllBe(x => x.Description == null);
        }

        [Test]
        public void Transactions_GetAllByAccountId_TransactionFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions?dateFrom=2014-04-01&amountFrom=100", id);

            var jSend = Server.GetJsendForCollection<TransactionModel>(url);
            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBe(1);
        }
    }
}