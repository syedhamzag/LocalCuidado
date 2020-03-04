using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class GetStaffPersonalInfo
    {
        public int StaffPersonalInfoId { get; set; }
        public string RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string ProfilePix { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
        public string Hobbies { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        public string Gender { get; set; }
        public string PostCode { get; set; }
        public decimal? Rate { get; set; }
        public string TeamLeader { get; set; }
        public string WorkTeam { get; set; }
        public string Passcode { get; set; }
        public string CanDrive { get; set; }
        public string DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        public string RightToWork { get; set; }
        public string RightToWorkAttachment { get; set; }
        public DateTime? RightToWorkExpiryDate { get; set; }
        public string DBS { get; set; }
        public string DBSAttachment { get; set; }
        public DateTime? DBSExpiryDate { get; set; }
        public string DBSUpdateNo { get; set; }
        public string NI { get; set; }
        public string NIAttachment { get; set; }
        public DateTime? NIExpiryDate { get; set; }

        public string CV { get; set; }
        public string CoverLetter { get; set; }
        public string Self_PYE { get; set; }
        public string Self_PYEAttachment { get; set; }
        public string Status { get; set; }
    }
}
