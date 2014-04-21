using NUnit.Framework;
using RestByDesign.Infrastructure.Extensions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RestByDesign.Tests.Infrastructure
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void FilterFields_FOrCollection_ShouldReturnNewObjectWithFieldsFiltered()
        {
            var instance = new TestClass();

            dynamic filteredObject = instance.SelectFields("Id,Dict,ListSubClasses,PrivateSetter,Dynamic");
            Type type = filteredObject.GetType();

            var properties = type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty)
                .Select(f => f.Name).ToList();

            properties.ShouldNotContain("PropToExclude");

            properties.ShouldContain("Id");
            properties.ShouldContain("Dict");
            properties.ShouldContain("ListSubClasses");
            properties.ShouldContain("PrivateSetter");

            Assert.AreEqual("1", filteredObject.Id);
            Assert.AreEqual("val1", filteredObject.Dict["key1"]);
            Assert.AreEqual("John", filteredObject.ListSubClasses[0].Name);
            Assert.AreEqual("abc", filteredObject.PrivateSetter);
            Assert.AreEqual(123, filteredObject.Dynamic);
        }

        private class TestClass
        {
            public TestClass()
            {
                Id = "1";
                Dict = new Dictionary<string, string> {{"key1", "val1"}};
                ListSubClasses = new[] { new TestSubClass() };
                PrivateSetter = "abc";
                Dynamic = 123;
            }

            public string Id { get; set; }
            public Dictionary<string,string> Dict { get; set; }
            public IEnumerable<TestSubClass> ListSubClasses { get; set; }
            public string PrivateSetter { get; private set; }
            public dynamic Dynamic { get; set; }

            // ReSharper disable once UnusedMember.Local
            public string PropToExclude { get; set; }
        }

        private class TestSubClass
        {
            public TestSubClass()
            {
                Name = "John";
            }

            public string Name { get; set; } 
        }
    }
}
