using System;
using System.Collections.Generic;
using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign.Infrastructure.Mappers
{
    public static class ModelMapper
    {
        private static readonly Dictionary<Type, object> Mappings = new Dictionary<Type, object>();

        static ModelMapper()
        {
            AddMapping<Client, ClientModel>(client => new ClientModel { Id = client.Id, Name = client.Name });
        }

        public static TTo Map<TFrom, TTo>(TFrom item) where TTo : class
        {
            if(item == null)
                throw new ArgumentNullException("item");

            var type = typeof (TFrom);

            if (Mappings.ContainsKey(type))
            {
                var mappingFunc = Mappings[type] as Func<TFrom, TTo>;
                return mappingFunc != null ? mappingFunc(item) : null;
            }

            throw new NotSupportedException();
        }

        private static void AddMapping<TFrom, TTo>(Func<TFrom, TTo> func) where TTo : class
        {
            var type = typeof (TFrom);

            if (Mappings.ContainsKey(type))
                throw new ArgumentException("Mapping is already configured");

            Mappings[type] = func;
        }
    }

}