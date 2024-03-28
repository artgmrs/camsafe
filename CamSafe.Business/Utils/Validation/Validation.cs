using CamSafe.Entity.CustomExceptions;
using System.Globalization;
using System.Net;

namespace CamSafe.Utils.Validation
{
    public static class Validation
    {
        public static void ValidateIpv4(string input)
        {
            IPAddress address;
            var isValid = IPAddress.TryParse(input, out address) && address.ToString() == input;

            if (!isValid) throw new CustomException($"Invalid IP value '{input}'");
        }

        public static void ValidateBoolean(string input)
        {
            var correctValues = new[] { "true", "false" };
            var isValid = correctValues.Any(x => x == input);

            if (!isValid) throw new CustomException($"Invalid BOOLEAN value '{input}'");
        }

        public static void ValidateInt(string input)
        {
            var isValid = int.TryParse(input, out _);

            if (!isValid) throw new CustomException($"Invalid ID value '{input}'");
        }

        public static void ValidateRangeDate(string start, string end)
        {
            var culture = CultureInfo.CreateSpecificCulture("pt-BR");

            DateTime startDate;
            if (!DateTime.TryParse(start, culture, out startDate))
            {
                throw new CustomException($"Invalid DATE value '{start}'.");
            }

            DateTime endDate;

            if (!DateTime.TryParse(end, culture, out endDate))
            {
                throw new CustomException($"Invalid DATE value '{end}'.");
            }

            if (startDate > endDate)
            {
                throw new CustomException($"Invalid RANGE DATE values. End date is bigger than start date.");
            }
        }

        public static void ValidateDateTimeFromString(string input)
        {
            var culture = CultureInfo.CreateSpecificCulture("pt-BR");
            if (!DateTime.TryParse(input, culture, out _))
            {
                throw new CustomException($"Invalid DATE value '{input}'.");
            }
        }

        public static void ValidateMoreThanOneTrueValue(bool value1, bool value2, bool value3)
        {
            bool[] conditions = { value1, value2, value3 };
            if (conditions.Count(cond => cond) > 1)
            {
                throw new CustomException("You can only use one filter at a time.");
            }
        }
    }
}
