using System.Diagnostics;
using System.Net.Http;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using RestByDesign.Infrastructure.JSend;

namespace RestByDesign.Tests.IntegrationTests.Helpers
{
    public static class IntegrationTestsExtensions
    {
        public static JSendPayload GetJsendObject(this TestServer server, string url)
        {
            var responseString = GetResponseString(server, url);

            return JsonConvert.DeserializeObject<JSendPayload>(responseString);
        }

        public static JSendPayloadTestHelper<T> GetJsendObject<T>(this TestServer server, string url) where T : class
        {
            var responseString = GetResponseString(server, url);

            return JsonConvert.DeserializeObject<JSendPayloadTestHelper<T>>(responseString);
        }

        public static JSendPayloadCollectionTestHelper<T> GetJsendForCollection<T>(this TestServer server, string url) where T : class
        {
            var responseString = GetResponseString(server, url);

            return JsonConvert.DeserializeObject<JSendPayloadCollectionTestHelper<T>>(responseString);
        }

        public static string GetResponseString(this TestServer server, string url)
        {
            var responseTask = server.GetResponse(url);
            var responseString = responseTask.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(responseString);
            return responseString;
        }

        public static HttpResponseMessage GetResponse(this TestServer server, string url)
        {
            var responseTask = server.HttpClient.GetAsync(url).Result;
            return responseTask;
        }
    }
}
