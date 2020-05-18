using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class PostStaffFullInfo
    {
        public PostStaffFullInfo()
        {
            StaffEducations = new List<PostStaffEducation>();
            StaffReferees = new List<PostStaffReferee>();
            StaffEducations = new List<PostStaffEducation>();
            StaffRegulatoryContacts = new List<PostStaffRegulatoryContact>();
            EmergencyContacts = new List<PostStaffEmergencyContact>();
        }
        public List<PostStaffTraining> StaffTrainings { get; set; }
        public List<PostStaffReferee> StaffReferees { get; set; }
        public List<PostStaffEducation> StaffEducations { get; set; }
        public List<PostStaffRegulatoryContact> StaffRegulatoryContacts { get; set; }
        public List<PostStaffEmergencyContact> EmergencyContacts { get; set; }
       
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string ProfilePix { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AboutMe { get; set; }
        [Required]
        public string Hobbies { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PostCode { get; set; }
        public decimal? Rate { get; set; }
        public string TeamLeader { get; set; }
        public string WorkTeam { get; set; }
        public string Passcode { get; set; }
        [Required]
        public string CanDrive { get; set; }
        public string DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        [Required]
        public string RightToWork { get; set; }
        public string RightToWorkAttachment { get; set; }
        public DateTime? RightToWorkExpiryDate { get; set; }
        [Required]
        public string DBS { get; set; }
        public string DBSAttachment { get; set; }
        public DateTime? DBSExpiryDate { get; set; }
        [Required]
        public string DBSUpdateNo { get; set; }
        [Required]
        public string NI { get; set; }
        public string NIAttachment { get; set; }
        public DateTime? NIExpiryDate { get; set; }
        [Required]
        public string CV { get; set; }
        [Required]
        public string CoverLetter { get; set; }
        [Required]
        public string Self_PYE { get; set; }
        public string Self_PYEAttachment { get; set; }


    }
}
