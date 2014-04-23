using System.ComponentModel.DataAnnotations;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    public class TransferModel : BaseModel
    {
        [Required]
        public string AccountIdFrom { get; set; }
        [Required]
        public string AccountIdTo { get; set; }
        [Min(0.01)]
        public decimal Amount { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}