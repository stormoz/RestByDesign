using System;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;
using Shouldly;

namespace RestByDesign.Tests
{
    public class MappingTests
    {
        public MappingTests()
        {
            MappingRegistration.RegisterMappings();
        }

        [Test]
        public void TestAllMappings()
        {
            AssertMappingCorrect<Client, ClientModel>((fr, to) => to.Id == fr.Id && fr.Name == to.Name);
        }

        private static void AssertMappingCorrect<TFrom, TTo> (Func<TFrom, TTo, bool> predicate = null)  where TFrom : class where TTo : class
        {
            var mapFrom = Create<TFrom>() as TFrom;
            var mapTo = ModelMapper.Map<TFrom, TTo>(mapFrom);

            mapTo.ShouldNotBe(null);
            mapTo.ShouldBeOfType<TTo>();

            if (predicate != null)
                predicate(mapFrom, mapTo).ShouldBe(true);
        }

        private static object Create<T>() where T : class
        {
            if (typeof (T) == typeof (Client))
                return new Client("1", "Foo", null);

            throw new ArgumentOutOfRangeException();
        }
    }
}
