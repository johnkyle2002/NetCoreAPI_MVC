using System.Globalization;

namespace NetCoreCommons.Extensions
{
    public static class StringExtension
    {         
        public static string ToTitleCase(this string value)
        {
            return string.IsNullOrEmpty(value) ? "" :
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }
    }
}
