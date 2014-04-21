using System;
using System.Collections.Generic;
using System.Linq;

namespace RestByDesign.Infrastructure.Mappers
{
    public static class ModelMapper
    {
        private static readonly Dictionary<Type, object> Mappings = new Dictionary<Type, object>();

        public static void AddMapping<TFrom, TTo>(Func<TFrom, TTo> func)
            where TFrom : class
            where TTo : class
        {
            var type = typeof (TFrom);

            if (Mappings.ContainsKey(type))
                throw new ArgumentException("Mapping is already configured");

            Mappings[type] = func;
        }

        public static TTo Map<TFrom, TTo>(TFrom item) 
            where TFrom : class
            where TTo : class
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var type = typeof(TFrom);

            if (!Mappings.ContainsKey(type))
                throw new NotSupportedException("Mapping is not set up for given type");

            var mappingFunc = Mappings[type] as Func<TFrom, TTo>;
            return mappingFunc != null ? mappingFunc(item) : null;
        }

        public static IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> items)
            where TFrom : class
            where TTo : class
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (!items.Any())
                yield break;

            var type = typeof(TFrom);

            if (!Mappings.ContainsKey(type))
                throw new NotSupportedException("Mapping is not set up for given type");

            var mappingFunc = Mappings[type] as Func<TFrom, TTo>;

            if (mappingFunc == null)
                yield break;

            foreach (var item in items)
            {
                yield return mappingFunc(item);
            }
        }
    }
}