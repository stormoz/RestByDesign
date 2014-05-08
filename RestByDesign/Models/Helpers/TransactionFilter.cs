using System;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Models.Enums;

namespace RestByDesign.Models.Helpers
{
    public class TransactionFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? AmountFrom { get; set; }
        public int? AmountTo { get; set; }
        public TransactionType? TransactionType { get; set; }

        public Expression<Func<Transaction, bool>> GetFilterExpression()
        {
            var exp = PredicateBuilder.True<Transaction>();

            if (TransactionType.HasValue && TransactionType.Value != Enums.TransactionType.All)
                exp = TransactionType.Value == Enums.TransactionType.Debit ? exp.And(t => t.Amount > 0) : exp.And(t => t.Amount < 0);

            if (DateFrom.HasValue)
                exp = exp.And(t => t.EffectDate >= DateFrom.Value);

            if (DateTo.HasValue)
                exp = exp.And(t => t.EffectDate <= DateTo.Value);

            if (AmountFrom.HasValue)
                exp = exp.And(t => Math.Abs(t.Amount) >= AmountFrom.Value);

            if (AmountTo.HasValue)
                exp = exp.And(t => Math.Abs(t.Amount) <= AmountTo.Value);

            return exp;
        }
    }
}