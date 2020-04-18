using System.Globalization;

namespace NetCoreMVC.Commons.Extensions
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
