using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
    public class LiveTracker
    {
        public LiveTracker()
        {
            TaskPerformed = new List<string>();
        }
        public int ClientRotaId { get; set; }
        public int StaffRotaId { get; set; }
        public int StaffRotaPeriodId { get; set; }
        public int ClientRotaDaysId { get; set; }
        public int ClientId { get; set; }
        public string Period { get; set; }
        public string ClientName { get; set; }
        public string ClientKeySafe { get; set; }
        public string ClientPostCode { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientProviderReference { get; set; }
        public string ClientIdNumber { get; set; }
        public string ClientLatitude { get; set; }
        public string ClientLongitude { get; set; }
        public decimal ClientRate { get; set; }
        public int NumberOfStaff { get; set; }
        public int AreaCode { get; set; }

        public string DayofWeek { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public DateTimeOffset? ClockInTime { get; set; }
        public DateTimeOffset? ClockOutTime { get; set; }
        public string ClockInMethod { get; set; }
        public string ClockOutMethod { get; set; }
        public string Feedback { get; set; }
        public string Rota { get; set; }
        public string Staff { get; set; }
        public string StaffTelephone { get; set; }
        public decimal? StaffRate { get; set; }
        public DateTime RotaDate { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public List<string> TaskPerformed { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
        public string Comment { get; set; }
        public string HandOver { get; set; }

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
                        rowColor = "#dff0d8";//green
                    }
                    else if (df > 15 && df <= 30)
                    {
                        rowColor = "#ADD8E6";

                    }
                    else if (df >= -30)
                    {
                        rowColor = "yellow";
                    }
                    else
                    {
                        rowColor = "#A9A9A9";// "#f2dede";//red
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
                return Math.Round((6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)))),2).ToString();
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
