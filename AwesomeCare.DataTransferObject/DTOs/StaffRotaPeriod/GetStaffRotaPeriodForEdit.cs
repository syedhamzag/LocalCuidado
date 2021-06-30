using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod
{
   public class GetStaffRotaPeriodForEdit
    {
        public int StaffRotaPeriodId { get; set; }
        public int StaffRotaId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
        public string Feedback { get; set; }
        public string Comment { get; set; }
        public string HandOver { get; set; }
        public int? ClientId { get; set; }
    }
}
