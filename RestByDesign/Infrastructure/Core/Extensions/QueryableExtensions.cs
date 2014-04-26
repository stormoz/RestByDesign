using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
    }
}