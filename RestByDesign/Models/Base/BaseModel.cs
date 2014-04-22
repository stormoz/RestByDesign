using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RestByDesign.Infrastructure.Attributes;
using RestByDesign.Infrastructure.Extensions;

namespace RestByDesign.Models.Base
{
    public class BaseModel
    {
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

        public void Patch(BaseModel u)
        {
            var nonPatchableAttr = typeof (NotPatchableAttribute);

            if (GetType().GetCustomAttributes(nonPatchableAttr).Any())
                return;

            var props = from p in GetType().GetProperties()
                        let attr = p.GetCustomAttribute(nonPatchableAttr)
                        where attr == null
                        select p;

            foreach (var prop in props)
            {
                var val = prop.GetValue(this, null);
                if (val != null)
                    prop.SetValue(u, val);
            }
        }
    }
}