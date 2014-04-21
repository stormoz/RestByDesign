using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendPayload
    {
        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public JSendStatus Status { get; set; }

        public object Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }
    }
}