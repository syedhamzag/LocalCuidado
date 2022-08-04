using AwesomeCare.DataTransferObject.DTOs.StaffOfficeLocation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.OfficeAttendance
{
    public class GetAttendance
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public List<GetStaffOfficeLocation> OfficeLocation { get; set; }
        public string JobTitle { get; set; }
        public int Staff { get; set; }
        public string StaffName { get; set; }
        public string Location { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockInDistance { get; set; }
        public string ClockOutAddress { get; set; }
        public string ClockOutDistance { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public DateTimeOffset? ClockIn { get; set; }
        public DateTimeOffset? ClockOut { get; set; }
        public string ClockInMethod { get; set; }
        public string ClockOutMethod { get; set; }
        public string ClockDiff { get; set; }
        public string Remark { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string RowClass(string startTime, DateTimeOffset? clockInTime)
        {
            try
            {
                //var st = TimeSpan.TryParseExact("6:15", "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out d) ? d : default(TimeSpan);
                //var ct = TimeSpan.TryParseExact("06:15:00", "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out c) ? c : default(TimeSpan);
                //var df = st.Subtract(ct);

                string rowColor = "gray";
                if (clockInTime.HasValue)
                {
                    var st = TimeSpan.TryParseExact(startTime, "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan d) ? d : default(TimeSpan);
                    var ct = TimeSpan.TryParseExact(clockInTime.Value.AddHours(1).DateTime.TimeOfDay.ToString(), "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan c) ? c : default(TimeSpan);
                    var df = st.Subtract(ct).TotalMinutes;

                    if (df <= 15 && df >= -15)
                    {
                        rowColor = "green";//green
                    }
                    else if (df > 15 && df <= 30)
                    {
                        rowColor = "gray";

                    }
                    else if (df >= -30)
                    {
                        rowColor = "yellow";
                    }
                    else
                    {
                        rowColor = "red";// "#f2dede";//red
                    }


                }
                else
                {
                    rowColor = "#f2dede";
                }

                return rowColor;//missed;
            }
            catch (Exception ex)
            {
                return "gray";

            }
        }
        public string CalculateDistance(Location point1, Location point2)
        {
            try
            {
                var d1 = point1.Latitude * (Math.PI / 180.0);
                var num1 = point1.Longitude * (Math.PI / 180.0);
                var d2 = point2.Latitude * (Math.PI / 180.0);
                var num2 = point2.Longitude * (Math.PI / 180.0) - num1;
                var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                         Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
                return Math.Round(((6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))) / 1000)), 2).ToString();
            }
            catch (Exception ex)
            {

                return "N/A";
            }
        }
    }
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
