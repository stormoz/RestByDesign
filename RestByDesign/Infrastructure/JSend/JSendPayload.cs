using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendPayload<T> where T : class
    {
        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public JSendStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual T Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }
    }
}