using System.Globalization;

namespace Terminplaner_be.Utility
{
    public class TimeValidator
    {
        public static bool IsValidTime(string time)
        {
            return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _);
        }
    }
}
