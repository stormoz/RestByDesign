using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;
using RestByDesign.Infrastructure.Core.Extensions;

namespace RestByDesign.Infrastructure.Core.Helpers
{
    public class FieldsHelper
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

        private static readonly Regex alphaNumeric = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Lazy<ModuleBuilder> moduleBuilder = new Lazy<ModuleBuilder>(CreateDynamicModule);
        private static readonly ConcurrentDictionary<string, Lazy<Type>> typesCache = new ConcurrentDictionary<string, Lazy<Type>>();

        /// <summary>
        /// Takes a given instance, creates and caches a new type based 
        /// on the instance's type and fields/props specified and creates 
        /// a new instance of the new type with values copied from the original instance
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="instance">Object to use as a base for a new instance</param>
        /// <param name="fields">Fields/props to leave and copy (not specified fields will be excluded)</param>
        /// <returns>New object with specified fields/values</returns>
        public static object Create<T>(T instance, IEnumerable<string> fields) where T : class
        {
            var fieldsList = PrepareFieldList<T>(fields);

            var type = typeof(T);
            var members = type.GetFields(bindingFlags).Where(p => fieldsList.Contains(p.Name)).Cast<MemberInfo>().ToList();
            members.AddRange(type.GetProperties(bindingFlags).Where(p => fieldsList.Contains(p.Name)).Cast<MemberInfo>().ToList());

            var newTypeName = GetTypeName(type, fieldsList);
            var newTypeLazy = typesCache.GetOrAdd(newTypeName, s => new Lazy<Type>(() => CreateNewType(members, newTypeName), LazyThreadSafetyMode.ExecutionAndPublication));
            var newType = newTypeLazy.Value;

            var newTypeInstance = Activator.CreateInstance(newType);

            foreach (var member in members)
            {
                var fieldInfo = newType.GetField(member.Name);
                var value = member.MemberType == MemberTypes.Property ? ((PropertyInfo)member).GetValue(instance) : ((FieldInfo)member).GetValue(instance);
                fieldInfo.SetValue(newTypeInstance, value);
            }

            return newTypeInstance;
        }

        private static List<string> PrepareFieldList<T>(IEnumerable<string> fields) where T : class
        {
            if (fields == null)
                throw new ArgumentNullException("fields");

            var fieldsList = fields.Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).OrderBy(x => x).ToList();

            if (fieldsList.Empty())
                throw new ArgumentException("Should not be empty.", "fields");

            if (fieldsList.Any(x => !alphaNumeric.IsMatch(x)))
                throw new ArgumentException("Fields should have alphanumeric names");

            return fieldsList;
        }

        private static string GetTypeName(Type type, IEnumerable<string> fieldsList)
        {
            return "Anon_" + string.Concat(type.FullName, "_", string.Join("_", fieldsList)).GetHashCode().ToString().Replace("-","_");
        }

        private static Type CreateNewType(IEnumerable<MemberInfo> members, string typeName)
        {
            var dynamicAnonymousType = moduleBuilder.Value.DefineType(typeName, TypeAttributes.Public);

            foreach (var member in members)
            {
                var memberType = member.MemberType == MemberTypes.Property ? ((PropertyInfo)member).PropertyType : ((FieldInfo)member).FieldType;
                dynamicAnonymousType.DefineField(member.Name, memberType, FieldAttributes.Public);
            }

            var newType = dynamicAnonymousType.CreateType();
            return newType;
        }

        private static ModuleBuilder CreateDynamicModule()
        {
            var moduleName = "DynamicModule" + Guid.NewGuid().ToString("n");

            var dynamicAssemblyName = new AssemblyName(moduleName);
            var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.Run);
            var dynamicModule = dynamicAssembly.DefineDynamicModule(moduleName);

            return dynamicModule;
        }
    }
}