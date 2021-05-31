using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.User
{
  public  class PutUser
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

       
        public bool EmailConfirmed { get; set; }

        public bool LockedOutEnabled { get; set; }

        [Required]
        [Display(Name = "Is Email Confirmed?")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Can User be locked Out?")]
        public string CanLockOut { get; set; }

       
        public string Password { get; set; }

        
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
