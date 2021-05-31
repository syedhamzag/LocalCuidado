using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.User
{
   public class PostResetPassord
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
