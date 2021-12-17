using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class GetStaffs
    {
        public int StaffPersonalInfoId { get; set; }
        public string Fullname { get; set; }
        public string RegistrationId { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string ApplicationUserId { get; set; }
        public bool CanDrive { get; set; }

        public string ProfilePix { get; set; }
    }
}
