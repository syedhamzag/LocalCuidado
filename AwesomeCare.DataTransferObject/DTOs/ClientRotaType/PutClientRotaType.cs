using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaType
{
   public class PutClientRotaType : BaseDTO
    {
        [Required]
        public int ClientRotaTypeId { get; set; }
        [Required]
        public string RotaType { get; set; }
    }
}
