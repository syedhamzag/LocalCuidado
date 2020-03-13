using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffEmergencyContact
    {
        public int StaffEmergencyContactId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string ContactName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Relationship { get; set; }
        public string Address { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
