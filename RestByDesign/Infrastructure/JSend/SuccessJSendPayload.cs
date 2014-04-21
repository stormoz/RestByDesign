using Newtonsoft.Json;

namespace RestByDesign.Infrastructure.JSend
{
    public class SuccessJSendPayload : JSendPayload
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public new object Data { get; set; }
    }
}