using System;
using Newtonsoft.Json;

namespace RestByDesign.Models.Helpers
{
    public class ApiError
    {
        public ApiError(Exception exception)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
            Source = exception.Source;
            if (exception.InnerException != null)
            {
                InnerError = new ApiError(exception.InnerException);
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ApiError InnerError { get; private set; }
    }
}