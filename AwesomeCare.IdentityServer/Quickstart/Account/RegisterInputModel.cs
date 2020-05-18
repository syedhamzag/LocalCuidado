using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.IdentityServer.Quickstart.Account
{
    public class RegisterInputModel
    {

        //
        // Summary:
        //     Gets or sets the email address for this user.
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the password for this user.
        [Required]
        public string Password { get; set; }
        //
        // Summary:
        //     Gets or sets the confirm password for this user.
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
