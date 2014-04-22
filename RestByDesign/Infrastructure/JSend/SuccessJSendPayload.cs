using Newtonsoft.Json;

namespace RestByDesign.Infrastructure.JSend
{
    public class SuccessJSendPayload : JSendPayload
    {
        private object _data;

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public new object Data
        {
            get { return _data; }
            set { _data = PrepareData(value); }
        }
    }
}