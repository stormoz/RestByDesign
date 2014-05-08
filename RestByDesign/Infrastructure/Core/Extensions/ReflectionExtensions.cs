using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RestByDesign.Infrastructure.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsExpandable<T>(this PropertyInfo p)
        {
            return (p.PropertyType.IsGenericType && p.PropertyType.GetInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>))) ||
                typeof(T).IsAssignableFrom(p.PropertyType);
        }
    }
}