using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecord
{
    public class PostBaseRecordItem
    {
        [Required]
        [MaxLength(225)]
        public string ValueName { get; set; }
        [Required]
        public bool Deleted { get; set; }
    }
}
