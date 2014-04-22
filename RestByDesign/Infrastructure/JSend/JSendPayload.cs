using System.Collections.Generic;
using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendPayload
    {
        private object _data;

        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public JSendStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data
        {
            get { return _data; }
            set { _data = PrepareData(value); }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }

        protected static object PrepareData(object responseObject)
        {
            return responseObject is IEnumerable<object> ?
                new CollectionWrapper((IEnumerable<object>)responseObject)
                : responseObject;
        }
    }
}