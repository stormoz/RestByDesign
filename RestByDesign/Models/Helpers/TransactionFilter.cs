using System;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model;
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
            Expression<Func<Transaction, bool>> exp = tr =>
                (DateFrom == null || tr.EffectDate >= DateFrom.Value) &&
                (DateTo == null || tr.EffectDate <= DateTo.Value) &&
                (AmountFrom == null || Math.Abs(tr.Amount) >= AmountFrom.Value) &&
                (AmountTo == null || Math.Abs(tr.Amount) <= AmountTo.Value) &&
                (TransactionType == null || TransactionType.Value == Enums.TransactionType.All ||
                     (TransactionType.Value == Enums.TransactionType.Debit && tr.Amount > 0) ||
                     (TransactionType.Value == Enums.TransactionType.Credit && tr.Amount < 0)
                    );
            return exp;
        }
    }
}