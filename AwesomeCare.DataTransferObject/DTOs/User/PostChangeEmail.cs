using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.User
{
   public class PostChangeEmail
    {
        [Required(ErrorMessage ="please provide userId")]
        public string UserId { get; set; }

        [Required(ErrorMessage ="please provide new email address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
