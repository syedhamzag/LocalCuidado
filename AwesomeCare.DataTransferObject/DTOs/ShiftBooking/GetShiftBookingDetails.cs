using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
   public class GetShiftBookingDetails
    {
        public int ShiftBookingId { get; set; }
        public string ShiftDate { get; set; }
        public int Rota { get; set; }
        public string RotaName { get; set; }
        public int NumberOfStaff { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// StaffPersonalInfoId
        /// </summary>
        public int Team { get; set; }
        public string TeamStaff { get; set; }
        public string DriverRequired { get; set; }
        public int? PublishTo { get; set; }
        public string PublishToWorkTeam { get; set; }
    }
}
