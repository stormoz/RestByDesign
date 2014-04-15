﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestByDesign.Models;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.DataAccess
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        internal  RestByDesignContext Context;
        internal DbSet<TEntity> DbSet;

        protected GenericRepository(RestByDesignContext context)
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
            IQueryable<TEntity> query = DbSet;

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
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "")
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = DbSet.Find(id);
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