using System;
using System.Linq;

namespace RestByDesign.Models.Helpers
{
    public class PagingInfo
    {
        private int _skip;
        private int _take;

        public PagingInfo()
        {
            Skip = 0;
            Take = 10;
        }

        public int Skip
        {
            get { return _skip; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("skip");

                _skip = value;
            }
        }

        public int Take
        {
            get { return _take; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("take");

                _take = value;
            }
        }

        public IQueryable<TEntity> GetPagedQuery<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            if(Skip > 0)
                query = query.Skip(Skip);

            return query.Take(Take);
        }
    }
}