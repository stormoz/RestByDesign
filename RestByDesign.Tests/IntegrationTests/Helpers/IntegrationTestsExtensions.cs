using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models.Helpers;
using RestByDesign.Tests.IntegrationTests.Core;

namespace RestByDesign.Tests.IntegrationTests.Helpers
{
    public static class IntegrationTestsExtensions
    {
        private static readonly JsonMediaTypeFormatter JsonFormatter = Startup.GlobalConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().Single();
        private static readonly JsonSerializerSettings JsonSerializerSettings = JsonFormatter.SerializerSettings;

        public static JSendPayload<T> GetJsendObject<T>(this TestServer server, string url, HttpVerbs httpMethod = HttpVerbs.Get, object body = null) where T : class
        {
            var responseString = GetResponseString(server, url, httpMethod, body);

            return JsonConvert.DeserializeObject<JSendPayload<T>>(responseString, JsonSerializerSettings);
        }

        public static JSendPayload<CollectionWrapper<T>> GetJsendForCollection<T>(this TestServer server, string url, HttpVerbs httpMethod = HttpVerbs.Get, object body = null) where T : class
        {
            var responseString = GetResponseString(server, url, httpMethod, body);

            return JsonConvert.DeserializeObject<JSendPayload<CollectionWrapper<T>>>(responseString, JsonSerializerSettings);
        }

        public static string GetResponseString(this TestServer server, string uri, HttpVerbs httpMethod = HttpVerbs.Get, object body = null)
        {
            var responseTask = GetResponse(server, uri, httpMethod, body);
            var responseString = responseTask.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(responseString);
            return responseString;
        }

        public static HttpResponseMessage GetResponse(this TestServer server, string uri, HttpVerbs httpMethod = HttpVerbs.Get, object body = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(httpMethod.ToString()), uri)
            {
                Content = new ObjectContent(typeof (object), body, JsonFormatter)
            };

            var responseTask = server.HttpClient.SendAsync(request).Result;
            return responseTask;
        }
    }
}

