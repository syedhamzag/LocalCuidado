using AwesomeCare.DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
    public class GetStaffProfile
    {
        public GetStaffProfile()
        {
            Education = new List<GetStaffEducation>();
            Trainings = new List<GetStaffTraining>();
            References = new List<GetStaffReferee>();
            RegulatoryContacts = new List<GetStaffRegulatoryContact>();
            EmergencyContacts = new List<GetStaffEmergencyContact>();
        }
        public List<GetStaffEducation> Education { get; set; }
        public List<GetStaffTraining> Trainings { get; set; }
        public List<GetStaffReferee> References { get; set; }
        public List<GetStaffRegulatoryContact> RegulatoryContacts { get; set; }
        public List<GetStaffEmergencyContact> EmergencyContacts { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string ApplicationUserId { get; set; }
        [Display(Name ="Staff Number")]
        public string RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        [Display(Name = "Profile Pix")]
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
        [Display(Name = "Can Drive?")]
        public string CanDrive { get; set; }
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }
        [Display(Name = "Driving License ExpiryDate")]
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        [Display(Name = "Right To Work?")]
        public string RightToWork { get; set; }
        [Display(Name = "Right To Work Attachment")]
        public string RightToWorkAttachment { get; set; }
        [Display(Name = "Right To Work ExpiryDate")]
        public DateTime? RightToWorkExpiryDate { get; set; }
        public string DBS { get; set; }
        [Display(Name = "DBS Attachment")]
        public string DBSAttachment { get; set; }
        [Display(Name = "DBS ExpiryDate")]
        public DateTime? DBSExpiryDate { get; set; }
        [Display(Name = "DBS UpdateNo")]
        public string DBSUpdateNo { get; set; }
        public string NI { get; set; }
        [Display(Name = "NI Attachment")]
        public string NIAttachment { get; set; }
        [Display(Name = "NI ExpiryDate")]
        public DateTime? NIExpiryDate { get; set; }

        public string CV { get; set; }
        public string CoverLetter { get; set; }
        [Display(Name = "Self/PYE")]
        public string Self_PYE { get; set; }
        [Display(Name = "Self/PYE Attachment")]
        public string Self_PYEAttachment { get; set; }
        public StaffRegistrationEnum Status { get; set; }
        [Display(Name = "Job Category")]
        public int? JobCategory { get; set; }
        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

       
        [Display(Name = "Is Team Leader?")]
        public string IsTeamLeader { get; set; }
       
        [Display(Name = "Has Uniform?")]
        public string HasUniform { get; set; }
       
        [Display(Name = "Has Id Card?")]
        public string HasIdCard { get; set; }
       
        [Display(Name = "Employment Date")]
        public DateTime? EmploymentDate { get; set; }
    }
}
