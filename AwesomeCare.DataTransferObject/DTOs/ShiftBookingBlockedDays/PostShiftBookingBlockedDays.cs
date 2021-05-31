using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays
{
   public class PostShiftBookingBlockedDays
    {
        [Required(ErrorMessage ="please provide shift booking")]
        public int ShiftBookingId { get; set; }
        [Required]
        [MaxLength(2)]
        public string Day { get; set; }
        [Required]
        [MaxLength(15)]
        public string WeekDay { get; set; }
    }
}
