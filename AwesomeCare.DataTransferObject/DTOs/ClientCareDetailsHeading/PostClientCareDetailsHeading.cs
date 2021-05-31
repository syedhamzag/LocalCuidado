using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading
{
   public class PostClientCareDetailsHeading:BaseDTO
    {
        [Required]
        [MaxLength(225)]
        public string Heading { get; set; }
    }
}
