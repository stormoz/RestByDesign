using System;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Infrastructure.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static ApiError ToApiError(this Exception exception)
        {
            return new ApiError(exception);
        }
    }
}