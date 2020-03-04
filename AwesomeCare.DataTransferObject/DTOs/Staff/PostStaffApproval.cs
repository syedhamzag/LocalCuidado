using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
  public  class PostStaffApproval
    {
        public int StaffPersonalInfoId { get; set; }
        public int? CommentBy_UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal? Rate { get; set; }
    }
}
