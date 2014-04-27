using System.Linq;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Models.Helpers;
using RestByDesign.Tests.BDD.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;
using TechTalk.SpecFlow;

namespace RestByDesign.Tests.BDD.Steps
{
    [Binding]
    public class TransactionHistorySteps : OwinBddTestBase
    {
        private JSendPayload<CollectionWrapper<TransactionModel>> jsendTransactions;

        [Given("I call get transactions for a certain accounts")]
        public void GetTransactions()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/transactions", id);

            jsendTransactions = Server.GetJsendForCollection<TransactionModel>(url);
        }

        [Then("the result should contains list of transactions")]
        public void ThenTheResultShouldBe()
        {
            jsendTransactions.Status.ShouldBe(JSendStatus.Success);
            jsendTransactions.Data.Items.Count().ShouldBeGreaterThan(0);
        }
    }
}
