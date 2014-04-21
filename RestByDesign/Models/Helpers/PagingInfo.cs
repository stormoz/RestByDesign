using System;
using System.Linq;

namespace RestByDesign.Models.Helpers
{
    public class PagingInfo
    {
        public PagingInfo()
        {
            Skip = 0;
            Take = 10;
        }

        public PagingInfo(int skip, int take)
        {
            if(take <= 0)
                throw new ArgumentOutOfRangeException("take");

            if (take < 0)
                throw new ArgumentOutOfRangeException("skip");

            Skip = skip;
            Take = take;
        }

        public int Skip { get; private set; }
        public int Take { get; private set; }

        public IQueryable<TEntity> GetPagedQuery<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            if(Skip > 0)
                query = query.Skip(Skip);

            return query.Take(Take);
        }
    }
}