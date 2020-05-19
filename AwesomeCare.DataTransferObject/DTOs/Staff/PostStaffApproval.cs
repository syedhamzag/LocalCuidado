using AwesomeCare.DataTransferObject.Enums;
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
        public StaffRegistrationEnum Status { get; set; }
        [Required]
        public decimal? Rate { get; set; }

        public bool? IsTeamLeader { get; set; }
        public bool? HasUniform { get; set; }
        public bool? HasIdCard { get; set; }
        public DateTime? EmploymentDate { get; set; }
    }
}
