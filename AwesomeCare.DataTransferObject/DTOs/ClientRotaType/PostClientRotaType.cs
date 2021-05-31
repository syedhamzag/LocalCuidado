using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaType
{
   public class PostClientRotaType : BaseDTO
    {
        [Required]
        public string RotaType { get; set; }
    }
}
