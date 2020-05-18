using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
   public class RotaAdmin
    {
        public RotaAdmin()
        {
            // RotaDays = new List<RotaDays>();
            Partners = new List<StaffPartner>();
        }
        public int ClientRotaId { get; set; }
        public int ClientId { get; set; }
        public string Period { get; set; }
        public string ClientName { get; set; }
        public string ClientKeySafe { get; set; }
        public string ClientPostCode { get; set; }

        //public List<RotaDays> RotaDays { get; set; }


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
