using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaName
{
   public class PostClientRotaName: BaseDTO
    {
        [Required]
        public int NumberOfStaff { get; set; }
        [Required]
        public string RotaName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Area { get; set; }
    }
}
