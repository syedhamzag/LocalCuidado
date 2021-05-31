using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class PutStaffRotaDynamicAddition:BaseDTO
    {
        [Required]
        public int StaffRotaDynamicAdditionId { get; set; }
        [Required]
        public string ItemName { get; set; }
    }
}
