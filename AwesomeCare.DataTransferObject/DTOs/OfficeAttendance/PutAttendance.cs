using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.OfficeAttendance
{
    public class PutAttendance
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public string JobTitle { get; set; }
        public int Staff { get; set; }
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
    }
}
