using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using PersonalBanking.Domain.Model.Core;

namespace RestByDesign.Infrastructure.Core.Extensions
{
    public static class QueryableExtensions
    {
        // Converts single instance to IQueryable
        public static SingleResult<T> Single<T>(this T instance) where T : class
        {
            return SingleResult.Create(new[] {instance}.AsQueryable());
        }

        // Converts collection to IQueryable
        public static IQueryable<T> List<T>(this IEnumerable<T> instance) where T : class
        {
            return instance.AsQueryable();
        }

        public static IQueryable<TEntity> WithIncluded<TEntity>(this IQueryable<TEntity> query, string includeProperties) where TEntity : class, IEntity
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var props = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                query = props.Aggregate(query, (current, prop) => current.Include(prop));
            }

            return query;
        }

        public static IQueryable<TEntity> WithIncluded<TEntity, TProperty>(this IQueryable<TEntity> query, params Expression<Func<TEntity, TProperty>>[] props) where TEntity : class, IEntity
        {
            if (props != null)
            {
                query = props.Aggregate(query, (current, prop) => current.Include(prop));
            }

            return query;
        }
    }
}