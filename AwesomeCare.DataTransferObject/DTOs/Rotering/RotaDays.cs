using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
  public  class RotaDays
    {
        public RotaDays()
        {
            Partners = new List<StaffPartner>();
        }
        public string DayofWeek { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }
        public string Rota { get; set; }
        public string Staff { get; set; }
        public DateTime RotaDate { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public List<StaffPartner> Partners { get; set; }
    }
}
