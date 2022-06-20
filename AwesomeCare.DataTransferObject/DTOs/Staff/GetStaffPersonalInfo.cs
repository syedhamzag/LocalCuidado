using AwesomeCare.DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class GetStaffPersonalInfo
    {
        public GetStaffPersonalInfo()
        {
            Education = new List<GetStaffEducation>();
            Trainings = new List<GetStaffTraining>();
            References = new List<GetStaffReferee>();
            EmergencyContacts = new List<GetStaffEmergencyContact>();
            RegulatoryContact = new List<GetStaffRegulatoryContact>();
        }
        public int StaffPersonalInfoId { get; set; }
        public string ApplicationUserId { get; set; }
        public string RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Display(Name ="Place of Birth")]
        public string PlaceOfBirth { get; set; }
        [Display(Name = "Date of Birth")]
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
        [Display(Name = "Driving License Expiry Date")]
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        [Display(Name ="Right to Work")]
        public string RightToWork { get; set; }
        [Display(Name = "Right to Work Attachment")]
        public string RightToWorkAttachment { get; set; }
        [Display(Name = "Right to Work Expiry Date")]
        public DateTime? RightToWorkExpiryDate { get; set; }
        public string DBS { get; set; }
        [Display(Name = "DBS Attachment")]
        public string DBSAttachment { get; set; }
        [Display(Name = "DBS Expiry Date")]
        public DateTime? DBSExpiryDate { get; set; }
        [Display(Name = "DBS Update No.")]
        public string DBSUpdateNo { get; set; }
        public string NI { get; set; }
        [Display(Name = "NI Attachment")]
        public string NIAttachment { get; set; }
        [Display(Name = "NI Expiry Date")]
        public DateTime? NIExpiryDate { get; set; }

        public string CV { get; set; }
        public string CoverLetter { get; set; }
        [Display(Name = "Selft PYE")]
        public string Self_PYE { get; set; }
        [Display(Name = "Selft PYE Attachment")]
        public string Self_PYEAttachment { get; set; }
        public StaffRegistrationEnum Status { get; set; }
        public int? JobCategory { get; set; }

        public int StaffWorkTeamId { get; set; }

        public bool? IsTeamLeader { get; set; }

        public bool? IsKeyWorker { get; set; }

        public bool? HasUniform { get; set; }
        
        public bool? HasIdCard { get; set; }
       
        public DateTime? EmploymentDate { get; set; }

        public List<GetStaffEducation> Education { get; set; }
        public virtual List<GetStaffTraining> Trainings { get; set; }
        public virtual List<GetStaffReferee> References { get; set; }
        public virtual List<GetStaffRegulatoryContact> RegulatoryContact { get; set; }
        public virtual List<GetStaffEmergencyContact> EmergencyContacts { get; set; }
    }
}
