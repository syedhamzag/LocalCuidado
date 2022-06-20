
using System.Globalization;

namespace System
{
    public static class DateTimeExtension
    {
   //     public static bool IsSameDay(this int day,string dayOfWeek,string month)
   //     {
			//try
			//{
   //             var currentYear = DateTime.Now.Year;
   //             int monthInt = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
   //             var date = new DateTime(currentYear, monthInt, day);
   //             var dayWeek = date.DayOfWeek.ToString();
   //             return string.Equals(dayWeek, dayOfWeek, StringComparison.InvariantCultureIgnoreCase);
   //         }
			//catch (Exception ex)
			//{
   //             return false;

			//}
   //     }

        public static DateTime ToPortalDateTime(this DateTime dateTime)
        {
            try
            {
             //   dateTime.Kind = DateTimeKind.Utc
                TimeZoneInfo ukZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, ukZone);
                return ukTime;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
