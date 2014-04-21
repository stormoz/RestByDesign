using Newtonsoft.Json.Converters;

namespace RestByDesign.Infrastructure.Core
{
    public class CamelCaseStringEnumConverter : StringEnumConverter
    {
        public CamelCaseStringEnumConverter()
        {
            CamelCaseText = true;
        }
    }
}