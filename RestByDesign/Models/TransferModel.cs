using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.Validation;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    public class TransferModel : BaseModel, IValidatableObject
    {
        [Required]
        public string AccountFrom { get; set; }
        [Required]
        public string AccountTo { get; set; }
        [Min(0.01)]
        public decimal Amount { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AccountFrom.EqualsIc(AccountTo))
            {
                yield return new ValidationResult("Transfer cannot be made to the same account.");
            }
        }
    }
}