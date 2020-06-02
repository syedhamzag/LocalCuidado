using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Communication
{
   public class PostCommunication
    {
       
        [Required]
        public string To { get; set; }
        [Required]
        public string Message { get; set; }
      
    }
}
