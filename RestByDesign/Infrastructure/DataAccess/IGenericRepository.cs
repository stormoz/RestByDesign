using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            PagingInfo pagingInfo = null,
            string includeProperties = "");

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        void Insert(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}