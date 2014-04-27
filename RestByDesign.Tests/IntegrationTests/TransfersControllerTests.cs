using System.Web.Mvc;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class TransfersControllerTests : OwinIntegrationTestBase
    {
        [Test]
        public void Transfers_Post()
        {
            DummyDataHelper.GetList<Transaction>().Count.ShouldBe(2);

            var id = "1";
            var url = string.Format("/api/clients/{0}/transfers", id);

            var jSend = Server.GetJsendObject<object>(url, HttpVerbs.Post, new { accountFrom = "111", accountTo = "222", amount = 1000, description = "car" });
            jSend.Status.ShouldBe(JSendStatus.Success);

            DummyDataHelper.GetList<Transaction>().Count.ShouldBe(4);
        }

        [Test]
        public void Transfers_PostToBigAmount_ShouldFail()
        {
            var id = "1";
            var url = string.Format("/api/clients/{0}/transfers", id);

            var jSend = Server.GetJsendObject<object>(url, HttpVerbs.Post, new { accountFrom = "111", accountTo = "222", amount = 11000, description = "car" });
            jSend.Status.ShouldBe(JSendStatus.Fail);
        }
    }
}