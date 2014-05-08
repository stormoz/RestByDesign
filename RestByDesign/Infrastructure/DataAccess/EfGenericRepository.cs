using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

using PersonalBanking.Domain.Model.Core;
using RestByDesign.Infrastructure.Core.Extensions;
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
#if DEBUG
            // Log EF activity
            Context.Database.Log = Log;
#endif
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity,bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "")
        {
            var query = filter != null ? DbSet.Where(filter) : DbSet.Select(x => x);

            query = query.WithIncluded(includeProperties);

            if (orderBy != null)
                query = orderBy(query);

            if (pagingInfo != null)
                query = pagingInfo.GetPagedQuery(orderBy != null ? query : query.OrderBy(x=>x.Id));

            return query.ToList();
        }

        public virtual TEntity GetSingle(
            Expression<Func<TEntity,bool>> filter,
            string includeProperties = "")
        {
            var query = DbSet.Where(filter);

            query = query.WithIncluded(includeProperties);

            return query.SingleOrDefault();
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter != null ? DbSet.Where(filter) : DbSet.Select(x => x);

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var entityToDelete = DbSet.SingleOrDefault(filter);

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

        private static void Log(string value)
        {
            Debug.WriteLine(value);
        }
    }
}