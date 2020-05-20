using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffBlackList
{
   public class PostStaffBlackList
    {
       
        [Required]
        [Display(Name = "Staff")]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
