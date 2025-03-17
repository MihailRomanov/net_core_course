using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Sample08_CustomBinding
{
    public record NormaLizedPhone(long Number)
    {
        public static bool TryParse(
            string? value,
            [NotNullWhen(true)]
            out NormaLizedPhone? phone)
        {
            value = Regex.Replace(value ?? "", @"[()\-+]", "");
            var result = long.TryParse(value, out var longValue);
            if (result)
                phone = new NormaLizedPhone(longValue);
            else
                phone = null;
            return result;
        }
    }

}
