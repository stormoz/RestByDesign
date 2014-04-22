namespace RestByDesign.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperFirstLetter(this string source)
        {
            return ConvertFirstLetter(source, false);
        }

        public static string ToLowerFirstLetter(this string source)
        {
            return ConvertFirstLetter(source);
        }

        private static string ConvertFirstLetter(this string source, bool lower = true)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            var letters = source.ToCharArray();
            letters[0] = lower ? char.ToLower(letters[0]) : char.ToUpper(letters[0]);
            return new string(letters);
        }
    }
}