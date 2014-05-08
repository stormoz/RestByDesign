using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Models.Helpers;
using RestByDesign.Infrastructure.Core.Extensions;

namespace RestByDesign.Infrastructure.DataAccess
{
    public class DummyGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

        private readonly IList<TEntity> list;
        private readonly IList<PropertyInfo> expandapleProps;

        public DummyGenericRepository(IList<TEntity> dymmyData)
        {
            list = dymmyData;
            expandapleProps = typeof(TEntity).GetProperties(bindingFlags).Where(p=>p.IsExpandable<TEntity>()).ToList();
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

            var listToReturn = (orderBy != null ? orderBy(query) : query);

            if (!listToReturn.Any())
                return listToReturn;

            ApplyExpand(listToReturn, includeProperties.SplitCsv());

            return listToReturn;
        }

        public virtual TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = "")
        {
            var item = list.SingleOrDefault(filter.Compile());

            if (item == null)
                return null;

            ApplyExpand(item, includeProperties.SplitCsv());

            return item;
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

        private void ApplyExpand(IEnumerable<TEntity> collection, IEnumerable<string> props)
        {
            foreach (var item in collection)
            {
                ApplyExpand(item, props);
            }
        }

        private void ApplyExpand(TEntity item, IEnumerable<string> props)
        {
            var propsNotEmpty = props.Any();

            foreach (var propInfo in expandapleProps)
            {
                if (propsNotEmpty && props.Contains(propInfo.Name))
                    continue;

                propInfo.SetValue(item, null);
            }
        }
    }
}