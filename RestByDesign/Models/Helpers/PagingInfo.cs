using System;
using System.Linq;

namespace RestByDesign.Models.Helpers
{
    public class PagingInfo
    {
        private int _skip;
        private int _take;

        public PagingInfo() : this(0, 10)
        { }

        public PagingInfo(int skip = 0, int take = 10)
        {
            Skip = skip;
            Take = take;
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

        public int? SkipNext(int total)
        {
            return Skip + Take >= total ? (int?)null : Skip + Take;
        }

        public int? PrevSkip(int total)
        {
            return Skip - Take < 0 ? (int?)null : Skip - Take;
        }

        public IQueryable<TEntity> GetPagedQuery<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            if(Skip > 0)
                query = query.Skip(Skip);

            return query.Take(Take);
        }
    }
}