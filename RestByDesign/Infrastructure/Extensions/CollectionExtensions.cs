using System;
using System.Collections.Generic;
using System.Linq;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<object> SelectFields<T>(this IEnumerable<T> list, string fieldsToInclude) where T : class
        {
            if (string.IsNullOrWhiteSpace(fieldsToInclude))
                return list;

            var fieldList = fieldsToInclude.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return fieldList.Any() ? list.Select(m => FieldsHelper.Create(m, fieldList)) : list;
        }

        public static object SelectFields<T>(this T item, string fieldsToInclude) where T : class
        {
            if (string.IsNullOrWhiteSpace(fieldsToInclude))
                return item;

            var fieldList = fieldsToInclude.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return fieldList.Any() ? FieldsHelper.Create(item, fieldList) : item;
        }

        public static bool Empty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }
    }
}