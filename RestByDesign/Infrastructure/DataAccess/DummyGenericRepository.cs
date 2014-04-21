using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyGenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private List<TEntity> list;

        public DummyGenericRepository(List<TEntity> dymmyData)
        {
            list = dymmyData;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = list.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var props = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                query = props.Aggregate(query, (current, prop) => current.Include(prop));
            }

            if (pagingInfo != null)
                query = pagingInfo.GetPagedQuery(query);

            return (orderBy != null ? orderBy(query) : query).ToList();
        }

        public virtual TEntity GetById(
            TKey id,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = list.AsQueryable();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var props = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = props.Aggregate(query, (current, prop) => current.Include(prop));
            }

            return query.Single(item => item.Equals(id));
        }

        public virtual void Insert(TEntity entity)
        {
            list.Add(entity);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = list.Single(item => item.Id.Equals(id));
            list.Remove(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            list.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            Delete(entityToUpdate.Id);
            Insert(entityToUpdate);
        }
    }
}