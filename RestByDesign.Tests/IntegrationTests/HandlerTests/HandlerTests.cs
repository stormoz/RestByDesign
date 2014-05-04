using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using System.Web.Mvc;
using Shouldly;
using System.Net;

namespace RestByDesign.Tests.IntegrationTests.HandlerTests
{
    public class HandlerTests : OwinIntegrationTestBase
    {
        [Test]
        public async Task TestHeadHandler()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);

            var getResponse = Server.GetResponse(url, HttpVerbs.Get);
            var getResult = await getResponse.Content.ReadAsStringAsync();

            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.Length.ShouldNotBe(0);

            var headResponse = Server.GetResponse(url, HttpVerbs.Head);
            var headResult = await headResponse.Content.ReadAsStringAsync();

            headResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            headResult.Length.ShouldBe(0);
        }

        [Test]
        public async Task TestCacheHandler()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);

            var response1 = Server.GetResponse(url);
            var response1Result = await response1.Content.ReadAsStringAsync();

            response1.StatusCode.ShouldBe(HttpStatusCode.OK);
            response1.Headers.ETag.ShouldNotBe(null);
            response1Result.Length.ShouldNotBe(0);

            var response2 = Server.GetResponse(url, editRequest: r => r.Headers.IfNoneMatch.Add(response1.Headers.ETag));
            var response2Result = await response2.Content.ReadAsStringAsync();
            
            response2.StatusCode.ShouldBe(HttpStatusCode.NotModified);
            response2Result.Length.ShouldBe(0);
        }

        [Test]
        public void TestCorsHandler()
        {
            var clientId = "1";
            var url = string.Format("/api/clients/{0}", clientId);

            var response = Server.GetResponse(url, HttpVerbs.Get, editRequest: r => {
                r.Headers.Add("Origin", "http://example.com");
                //r.Headers.Add("Access-Control-Request-Method", "POST");
            });

            response.Headers.ShouldContain(h => h.Key.Equals("Access-Control-Allow-Origin"));
        }

        [Test]
        public void TestOptionsHandler()
        {
            var url = string.Format("/api/smarttags");

            var response = Server.GetResponse(url, HttpVerbs.Options);

            var supportedMethods = response.Headers.First(h => h.Key.EqualsIc("Access-Control-Allow-Methods")).Value.First();
            supportedMethods.ShouldContain("GET");
            supportedMethods.ShouldContain("PATCH");
            supportedMethods.ShouldContain("DELETE");

            supportedMethods.ShouldNotContain("POST");
        }
    }
}
