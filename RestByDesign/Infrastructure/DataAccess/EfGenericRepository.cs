using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class EfGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        internal  RestByDesignContext Context;
        internal DbSet<TEntity> DbSet;

        public EfGenericRepository(RestByDesignContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity,bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "")
        {
            var query = DbSet.AsExpandable();

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
            Expression<Func<TEntity,bool>> filter,
            string includeProperties = "")
        {
            var query = DbSet.AsExpandable();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var props = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = props.Aggregate(query, (current, prop) => current.Include(prop));
            }

            return query.SingleOrDefault(filter);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entityToDelete = DbSet.SingleOrDefault(filter);

            if(filter != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
                DbSet.Attach(entityToDelete);

            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}