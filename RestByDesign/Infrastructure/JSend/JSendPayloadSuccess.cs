using Newtonsoft.Json;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendPayloadSuccess<T> : JSendPayload<T> where T : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public override T Data { get; set; }
    }
}