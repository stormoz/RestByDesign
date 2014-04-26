using System.ComponentModel.DataAnnotations;
using RestByDesign.Infrastructure.Core;
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
        [Required]
        public bool? Active { get; set; } //field made nullable just to demo Validation on Patch
    }
}