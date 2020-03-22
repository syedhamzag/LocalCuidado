using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
    public class PostShiftBooking
    {
        [Required]
        [Display(Name = "Shift Date")]
        public string ShiftDate { get; set; }
        [Required]
        public int Rota { get; set; }
        [Required]
        [Display(Name = "Number Of Staff")]
        public int NumberOfStaff { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string StopTime { get; set; }
        [Required]
        public string Remark { get; set; }
        /// <summary>
        /// StaffPersonalInfoId
        /// </summary>
        [Required]
        public int Team { get; set; }
        [Required]
        [Display(Name = "Driver Required?")]
        public bool DriverRequired { get; set; }
        [Display(Name = "Publish To")]
        public string PublishTo { get; set; }
    }
}
