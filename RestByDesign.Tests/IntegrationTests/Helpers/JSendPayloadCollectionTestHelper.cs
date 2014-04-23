using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.JSend;

namespace RestByDesign.Tests.IntegrationTests.Helpers
{
    public class JSendPayloadCollectionTestHelper<T> where T : class
    {
        private CollectionWrapperTestHelper<T> _data;

        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public JSendStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CollectionWrapperTestHelper<T> Data
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