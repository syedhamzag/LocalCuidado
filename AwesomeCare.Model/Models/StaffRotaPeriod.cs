﻿using System;
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
        public string Feedback { get; set; }
        public string Comment { get; set; }
        public string HandOver { get; set; }
        public string ClockInMode { get; set; }
        public string ClockOutMode { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int? ClientId { get; set; }
        public virtual StaffRota StaffRota { get; set; }
        public virtual ClientRotaType ClientRotaType { get; set; }
        public virtual ICollection<StaffRotaTask> StaffRotaTasks { get; set; }
    }
}
