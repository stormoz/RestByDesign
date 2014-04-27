using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IList<TEntity> list;

        public DummyGenericRepository(IList<TEntity> dymmyData)
        {
            list = dymmyData;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "")
        {
            var query = list.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (pagingInfo != null)
                query = pagingInfo.GetPagedQuery(query);

            return (orderBy != null ? orderBy(query) : query).ToList();
        }

        public virtual TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = "")
        {
            return list.SingleOrDefault(filter.Compile());
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = list.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public virtual void Insert(TEntity entity)
        {
            list.Add(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entityToDelete = list.Single(filter.Compile());
            list.Remove(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            list.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            Delete(entityToUpdate);
            Insert(entityToUpdate);
        }
    }
}