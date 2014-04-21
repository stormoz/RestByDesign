using System;
using System.Collections.Generic;
using System.Linq;

namespace RestByDesign.Models.Helpers
{
    public class Fields<T> where T : class
    {
        public List<string> FieldList { get; private set; }
        public T Model { get; private set; }

        public Fields(string fields, T model)
        {
            if(string.IsNullOrWhiteSpace(fields))
                throw new ArgumentNullException("fields");

            FieldList = fields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Model = model;
        }
    }
}