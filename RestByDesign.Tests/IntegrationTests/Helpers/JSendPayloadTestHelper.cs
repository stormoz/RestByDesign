using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.JSend;

namespace RestByDesign.Tests.IntegrationTests.Helpers
{
    public class JSendPayloadTestHelper<T> where T : class
    {
        private T _data;

        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public JSendStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }
    }
}