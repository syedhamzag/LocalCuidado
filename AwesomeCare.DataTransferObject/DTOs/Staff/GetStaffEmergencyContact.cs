using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class GetStaffEmergencyContact
    {
        public int StaffEmergencyContactId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string ContactName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Relationship { get; set; }
        public string Address { get; set; }
    }
}
