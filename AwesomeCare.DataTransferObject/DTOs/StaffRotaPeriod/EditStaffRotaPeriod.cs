using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod
{
  public  class EditStaffRotaPeriod
    {
        [Required]
        public int StaffRotaPeriodId { get; set; }
        [Required]
        [Display(Name = "Clock In Time")]
        [MaxLength(15)]
        public string ClockInTime { get; set; }
        [Required]
        [Display(Name = "Clock Out Time")]
        [MaxLength(15)]
        public string ClockOutTime { get; set; }
        [Display(Name = "Clock In Address")]
        [MaxLength(100)]
        public string ClockInAddress { get; set; }
        [Display(Name = "Clock Out Address")]
        [MaxLength(100)]
        public string ClockOutAddress { get; set; }
        [MaxLength(225)]
        public string Feedback { get; set; }
        [MaxLength(225)]
        [Display(Name = "Reply/Remark/Comment")]
        public string Comment { get; set; }
        [MaxLength(225)]
        [Display(Name = "Handover Note")]
        public string HandOver { get; set; }

        public int? ClientId { get; set; }
    }
}
