using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RestByDesign.Models.Helpers
{
    public class CollectionWrapper
    {
        public CollectionWrapper(IEnumerable<object> items, PagingInfo pagingInfo = null)
        {
            Items = items;
            Prev = pagingInfo == null || pagingInfo.Skip == 0? (int?)null: pagingInfo.Skip;
            Next = pagingInfo == null ? (int?)null : pagingInfo.Take;
        }

        public IEnumerable<object> Items { get; set; }
        public int Count { get { return Items.Count(); } }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Prev { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Next { get; private set; }
    }
}