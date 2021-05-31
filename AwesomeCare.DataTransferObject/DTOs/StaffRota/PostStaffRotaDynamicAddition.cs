using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class PostStaffRotaDynamicAddition: BaseDTO
    {
        [Required]
        public string ItemName { get; set; }
    }
}
