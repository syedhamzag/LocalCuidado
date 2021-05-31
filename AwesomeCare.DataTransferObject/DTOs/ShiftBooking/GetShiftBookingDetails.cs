using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
   public class GetShiftBookingDetails
    {
        public int ShiftBookingId { get; set; }
        [Display(Name = "Shift Date")]
        public string ShiftDate { get; set; }
        public int Rota { get; set; }
        [Display(Name = "RotaName")]
        public string RotaName { get; set; }
        [Display(Name = "Number of Staff")]
        public int NumberOfStaff { get; set; }
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }
        [Display(Name = "Stop Time")]
        public string StopTime { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// StaffPersonalInfoId
        /// </summary>
        public int Team { get; set; }

        [Display(Name = "Team Staff")]
        public string TeamStaff { get; set; }

        [Display(Name = "Driver Required?")]
        public string DriverRequired { get; set; }

        [Display(Name = "Publish To")]
        public int? PublishTo { get; set; }

        [Display(Name = "Publish To Work Team")]
        public string PublishToWorkTeam { get; set; }
    }
}
