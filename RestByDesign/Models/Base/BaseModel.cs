using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.Core.Extensions;

namespace RestByDesign.Models.Base
{
    public class BaseModel
    {
        /// <summary>
        /// Example of a simple patch implementation
        /// </summary>
        /// <param name="u">Object</param>
        public void PatchFrom(Object u)
        {
            if (u == null)
                return;

            var jObject = u as JObject;
            if(jObject == null)
                throw new NotSupportedException("Only Newtonsoft.Json formatter supported");

            var nonPatchableAttr = typeof(NotPatchableAttribute);

            if (GetType().GetCustomAttributes(nonPatchableAttr).Any())
                return;

            var props = GetType().GetProperties().Where(p => p.GetCustomAttribute(nonPatchableAttr) == null).ToList();

            foreach (var prop in props)
            {
                var token = jObject.SelectToken(prop.Name.ToLowerFirstLetter());

                if(token == null)
                    continue;

                var type = prop.PropertyType;

                prop.SetValue(this, Convert.ChangeType(token, type));
            }
        }
    }
}