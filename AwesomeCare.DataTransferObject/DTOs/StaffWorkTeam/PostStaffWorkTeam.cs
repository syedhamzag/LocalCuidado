using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam
{
    public class PostStaffWorkTeam
    {
        [Required]
        [MaxLength(100)]
        public string WorkTeam { get; set; }
    }
}
