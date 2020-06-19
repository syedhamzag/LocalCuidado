using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Communication
{
    public class PostCommunication
    {

        [Required(ErrorMessage ="please select recipient")]
        public string To { get; set; }
        [Required(ErrorMessage ="please compose a message")]
        public string Message { get; set; }
        [Required(ErrorMessage ="please provide subject")]
        [MaxLength(125)]
        public string Subject { get; set; }
    }
}
