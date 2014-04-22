using System;
using System.Collections.Generic;
using System.Linq;

namespace RestByDesign.Infrastructure.Mappers
{
    public static class ModelMapper
    {
        private static readonly Dictionary<KeyValuePair<Type, Type>, object> MappingsObjectToNew = new Dictionary<KeyValuePair<Type, Type>, object>();
        private static readonly Dictionary<KeyValuePair<Type, Type>, object> MappingsObjectToObject = new Dictionary<KeyValuePair<Type, Type>, object>();

        public static void AddMapping<TFrom, TTo>(Func<TFrom, TTo> func)
            where TFrom : class
            where TTo : class
        {
            var key = GetKey<TFrom, TTo>();

            if (MappingsObjectToNew.ContainsKey(key))
                throw new ArgumentException("Mapping is already configured");

            MappingsObjectToNew[key] = func;
        }

        public static void AddMapping<TFrom, TTo>(Action<TFrom, TTo> func)
            where TFrom : class
            where TTo : class
        {
            var key = GetKey<TFrom, TTo>();

            if (MappingsObjectToObject.ContainsKey(key))
                throw new ArgumentException("Mapping is already configured");

            MappingsObjectToObject[key] = func;
        }

        public static TTo Map<TFrom, TTo>(TFrom item) 
            where TFrom : class
            where TTo : class
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var key = GetKey<TFrom, TTo>();

            if (!MappingsObjectToNew.ContainsKey(key))
                throw new NotSupportedException("Mapping is not set up for given type");

            var mappingFunc = MappingsObjectToNew[key] as Func<TFrom, TTo>;
            return mappingFunc != null ? mappingFunc(item) : null;
        }

        public static void Map<TFrom, TTo>(TFrom itemFrom, TTo itemTo)
            where TFrom : class
            where TTo : class
        {
            if (itemFrom == null)
                throw new ArgumentNullException("itemFrom");

            if (itemTo == null)
                throw new ArgumentNullException("itemTo");

            var key = GetKey<TFrom, TTo>();

            if (!MappingsObjectToObject.ContainsKey(key))
                throw new NotSupportedException("Mapping is not set up for given type");

            var mappingFunc = MappingsObjectToObject[key] as Action<TFrom, TTo>;
            mappingFunc(itemFrom, itemTo);
        }

        public static IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> items)
            where TFrom : class
            where TTo : class
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (!items.Any())
                yield break;

            var key = GetKey<TFrom, TTo>();

            if (!MappingsObjectToNew.ContainsKey(key))
                throw new NotSupportedException("Mapping is not set up for given type");

            var mappingFunc = MappingsObjectToNew[key] as Func<TFrom, TTo>;

            if (mappingFunc == null)
                yield break;

            foreach (var item in items)
            {
                yield return mappingFunc(item);
            }
        }

        private static KeyValuePair<Type, Type> GetKey<TFrom, TTo>() where TFrom : class where TTo : class
        {
            return new KeyValuePair<Type, Type>(typeof(TFrom),typeof(TTo));
        }
    }
}