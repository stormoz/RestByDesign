using System;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models;
using Shouldly;

namespace RestByDesign.Tests.UnitTests
{
    public class MappingTests
    {
        [SetUp]
        public void Init()
        {
            MappingRegistration.RegisterMappings();
        }

        [Test]
        public void TestAllMappings()
        {
            // Map to a new object
            var client = new Client("1", "Foo");
            AssertMappingCorrect<Client, ClientModel>(client, (fr, to) => to.Id == fr.Id && fr.Name == to.Name);

            // Map to an existing object
            var smartTagModel = new SmartTagModel {Id = "1", Active = true};
            var smartTag = new SmartTag ("1", "111", false, false, 1, new DateTime());
            AssertMappingCorrect(smartTagModel, smartTag, (fr, to) => fr.Active == to.Active);
        }

        private static void AssertMappingCorrect<TFrom, TTo> (TFrom mapFrom, Func<TFrom, TTo, bool> predicate = null)  where TFrom : class where TTo : class
        {
            var mapTo = ModelMapper.Map<TFrom, TTo>(mapFrom);

            AssertMapping(mapFrom, predicate, mapTo);
        }

        private static void AssertMappingCorrect<TFrom, TTo>(TFrom mapFrom, TTo mapTo, Func<TFrom, TTo, bool> predicate = null)
            where TFrom : class
            where TTo : class
        {
            ModelMapper.Map(mapFrom, mapTo);

            AssertMapping(mapFrom, predicate, mapTo);
        }

        private static void AssertMapping<TFrom, TTo>(TFrom mapFrom, Func<TFrom, TTo, bool> predicate, TTo mapTo) where TFrom : class
            where TTo : class
        {
            mapTo.ShouldNotBe(null);
            mapTo.ShouldBeOfType<TTo>();

            if (predicate != null)
                predicate(mapFrom, mapTo).ShouldBe(true);
        }
    }
}
