using System.Collections.Generic;
using Newtonsoft.Json;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    [NotPatchable]
    public class ClientModel : BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<AccountModel> Accounts { get; set; }
    }
}