using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.OfficeAttendance
{
    public class CreateAttendance
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
        public List<SelectListItem> StaffList { get; set; } = new List<SelectListItem>();
    }
}
