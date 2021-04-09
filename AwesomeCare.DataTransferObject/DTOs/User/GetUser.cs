using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.User
{
   public class GetUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockedOutEnabled { get; set; }

    }
}
