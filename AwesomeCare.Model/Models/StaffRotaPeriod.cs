using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffRotaPeriod
    {
        public StaffRotaPeriod()
        {
            StaffRotaTasks = new HashSet<StaffRotaTask>();
        }
        public int StaffRotaPeriodId { get; set; }
        public int StaffRotaId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public DateTimeOffset? ClockInTime { get; set; }
        public DateTimeOffset? ClockOutTime { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
        public string ClockInDistance { get; set; }
        public string ClockOutDistance { get; set; }
        public int? ClockInCount { get; set; }
        public int? ClockOutCount { get; set; }
        public string ClockInGeolocation { get; set; }
        public string ClockOutGeolocation { get; set; }
        public string Feedback { get; set; }
        public string Comment { get; set; }
        public string HandOver { get; set; }
        public string ClockInMode { get; set; }
        public string ClockOutMode { get; set; }
        public string ClockInClientTelephone { get; set; }
        public string ClockOutClientTelephone { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int? ClientId { get; set; }
        public string BowelMovement { get; set; }
        public string OralCare { get; set; }
        public string FluidIntake { get; set; }
        public virtual StaffRota StaffRota { get; set; }
        public virtual ClientRotaType ClientRotaType { get; set; }
        public virtual ICollection<StaffRotaTask> StaffRotaTasks { get; set; }
    }
}
