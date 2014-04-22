using System;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    public class TransactionModel : BaseModel
    {
        public string Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? EffectDate { get; set; }
    }
}