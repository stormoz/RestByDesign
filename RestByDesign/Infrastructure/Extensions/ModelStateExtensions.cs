using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace RestByDesign.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState)
        {
            foreach (var val in modelState.Values)
            {
                foreach (var error in val.Errors)
                {
                    yield return error.ErrorMessage;
                }
            }
        }

        public static string FlattenErrors(this ModelStateDictionary modelState)
        {
            return string.Join(Environment.NewLine, modelState.Errors());
        }
    }
}