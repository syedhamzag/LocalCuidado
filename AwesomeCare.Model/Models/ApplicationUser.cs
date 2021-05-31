

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
           
        }
       // public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
