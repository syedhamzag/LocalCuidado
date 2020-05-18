using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
   public class GetShiftBooking
    {
        public int ShiftBookingId { get; set; }
        public string ShiftDate { get; set; }
        public int Rota { get; set; }
        public int NumberOfStaff { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// StaffPersonalInfoId
        /// </summary>
        public int Team { get; set; }
        public bool DriverRequired { get; set; }
        public string PublishTo { get; set; }
    }
}
