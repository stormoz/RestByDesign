using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestByDesign.Models.Helpers
{
    public class CollectionWrapper<T> where T : class
    {
        public CollectionWrapper(IEnumerable<T> items, int? count = null, string prev = null, string next = null)
        {
            Items = items;
            Count = count;
            Prev = prev;
            Next = next;
        }

        public IEnumerable<T> Items { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Prev { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Next { get; private set; }
    }
}
