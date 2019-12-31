using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecordItem
{
   public class BaseRecordItemDto
    {
        [Required]
        [MaxLength(225)]
        public string ValueName { get; set; }
                
    }
}
