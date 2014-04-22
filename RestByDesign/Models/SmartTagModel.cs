
using System.ComponentModel.DataAnnotations;
using RestByDesign.Infrastructure.Attributes;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    public class SmartTagModel : BaseModel
    {
        [NotPatchable]
        public string Id { get; set; }
        //[ConcurrencyCheck]
        [NotPatchable]
        public int? Version { get; set; }
        public bool? Active { get; set; }
    }
}