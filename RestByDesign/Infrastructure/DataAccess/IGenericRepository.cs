using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models;

namespace RestByDesign.Infrastructure.DataAccess
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "");

        TEntity GetById(TKey id,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "");

        void Insert(TEntity entity);
        void Delete(TKey id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}