using System.Globalization;

namespace CamSafe.Business.Utils.Conversion
{
    public static class Conversion
    {
        public static DateTime ConvertStringToDateTime(string input)
        {
            var culture = CultureInfo.CreateSpecificCulture("pt-BR");
            DateTime date = DateTime.Parse(input, culture);

            return date;
        }
    }
}
