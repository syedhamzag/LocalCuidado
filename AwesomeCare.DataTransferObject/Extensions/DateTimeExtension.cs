
using System.Globalization;

namespace System
{
    public static class DateTimeExtension
    {
        public static bool IsSameDay(this int day, string dayOfWeek, string month)
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                int monthInt = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                var date = new DateTime(currentYear, monthInt, day);
                var dayWeek = date.DayOfWeek.ToString();
                return string.Equals(dayWeek, dayOfWeek, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public static string ToMonthName (this string monthYear)
        {
            try
            {
                var splittedDate = monthYear.Split('/');
                int month = int.Parse(splittedDate[0]);
                int year = int.Parse(splittedDate[1]);
                var date = new DateTime(year, month, 1);
                return date.ToString("MMMM", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        public static string ToMonthName(this string month,int year)
        {
            try
            {
                var mnt = int.Parse(month);
                var date = new DateTime(year, mnt, 1);
                return date.ToString("MMMM", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return "";

            }
        }
    }
}
