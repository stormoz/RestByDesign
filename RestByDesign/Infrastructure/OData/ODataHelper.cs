using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestByDesign.Infrastructure.OData
{
    public static class ODataHelper
    {
        public static SingleResult<T> Single<T>(T instance) where T : class
        {
            return SingleResult.Create(new[] {instance}.AsQueryable());
        }

        public static IQueryable<T> List<T>(IEnumerable<T> instance) where T : class
        {
            return instance.AsQueryable();
        }
    }
}