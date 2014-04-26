using System.Linq;
using Microsoft.Owin.Testing;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Core;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;
using TechTalk.SpecFlow;

namespace RestByDesign.Tests.BDD
{
    [Binding]
    public class TransactionHistorySteps
    {
        private TestServer _server;
        private JSendPayloadCollectionTestHelper<TransactionModel> jsendTransactions;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _server = TestServer.Create<Startup>();
        }

        [Given("I call get transactions for a certain accounts")]
        public void GetTransactions()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions", id);

            jsendTransactions = _server.GetJsendForCollection<TransactionModel>(url);
        }

        [Then("the result should contains list of transactions")]
        public void ThenTheResultShouldBe()
        {
            jsendTransactions.Status.ShouldBe(JSendStatus.Success);
            jsendTransactions.Data.Items.Count().ShouldBeGreaterThan(0);
        }
    }
}
