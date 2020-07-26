using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.DataTransferObject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
    public class PutStaffPersonalInfo
    {
        public PutStaffPersonalInfo()
        {
            Education = new List<PutStaffEducation>();
            Trainings = new List<PutStaffTraining>();
            References = new List<PutStaffReferee>();
            EmergencyContacts = new List<PutStaffEmergencyContact>();
        }

        public List<PutStaffEducation> Education { get; set; }
        public List<PutStaffTraining> Trainings { get; set; }
        public List<PutStaffReferee> References { get; set; }
        public List<PutStaffEmergencyContact> EmergencyContacts { get; set; }
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        [Required]
        public string Telephone { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
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
        [Display(Name ="Team Leader")]
        public string TeamLeader { get; set; }
        public string WorkTeam { get; set; }
        [Required]
        [MaxLength(4)]
        [Display(Name = "Passcode/PIN max = 4 ")]
        public string Passcode { get; set; }
        [Required]
        [Display(Name = "Can you drive?")]
        public string CanDrive { get; set; }

        [Display(Name = "Attach Driving License if you can drive")]
       // [RequiredDependant("Yes", nameof(CanDrive), typeof(string), ErrorMessage = "Please attach Driving License if you can drive")]
        public string DrivingLicense { get; set; }

        [Display(Name = "Driving License ExpiryDate")]
      //  [RequiredDependant("Yes", nameof(CanDrive), typeof(string), ErrorMessage = "Please provide Driving License Expiry Date if you can drive")]
        public DateTime? DrivingLicenseExpiryDate { get; set; }

        [Required]
        [Display(Name = "Right to work?")]
        public string RightToWork { get; set; }

        [Display(Name = "Right to work attachment")]
      //  [RequiredDependant("Yes", nameof(RightToWork), typeof(string), ErrorMessage = "Please attach Right to Work")]
        public string RightToWorkAttachment { get; set; }

        [Display(Name = "Right to work ExpiryDate")]
       // [RequiredDependant("Yes", nameof(RightToWork), typeof(string), ErrorMessage = "Please provice Right to Work Expiry Date")]
        public DateTime? RightToWorkExpiryDate { get; set; }

        [Required]
        public string DBS { get; set; }

        [Display(Name = "DBS attachment")]
       // [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please attach DBS")]
        public string DBSAttachment { get; set; }

        [Display(Name = "DBS ExpiryDate")]
       // [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please provide DBS Expiry Date")]
        public DateTime? DBSExpiryDate { get; set; }

        [Display(Name = "DBS Update No")]
       // [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please provide DBS Update No.")]
        public string DBSUpdateNo { get; set; }

        [Required]
        [Display(Name = "NI")]
        public string NI { get; set; }

        [Display(Name = "NI attachment")]
        //[RequiredDependant("Yes", nameof(NI), typeof(string), ErrorMessage = "Please attach NI")]
        public string NIAttachment { get; set; }

        [Display(Name = "NI expiry date")]
       // [RequiredDependant("Yes", nameof(NI), typeof(string), ErrorMessage = "Please provide NI Expiry Date")]
        public DateTime? NIExpiryDate { get; set; }

        [Required]
        public string CV { get; set; }

        [Required]
        public string CoverLetter { get; set; }

        [Required]
        [Display(Name = "Self PYE")]
        public string SelfPYE { get; set; }

        [Display(Name = "Self PYE attachment")]
       // [RequiredDependant("Yes", nameof(SelfPYE), typeof(string), ErrorMessage = "Please attach Self PYE")]
        public string SelfPYEAttachment { get; set; }

        public StaffRegistrationEnum Status { get; set; }
        [Required]
        public string RegistrationId { get; set; }

       [Display(Name ="Is Team Lead?")]
        public bool? IsTeamLeader { get; set; }
      
        [Display(Name ="Has Uniform?")]
        public bool? HasUniform { get; set; }

        [Display(Name ="Has Id Card?")]
        [Required]
        public bool? HasIdCard { get; set; }
       
        [Display(Name ="Employment Date")]
        public DateTime? EmploymentDate { get; set; }

        [Display(Name ="Job Category")]
        [Required]
        public int? JobCategory { get; set; }

        [Display(Name ="Place of Birth")]
        [Required]
        public string PlaceOfBirth { get; set; }
        public int? StaffWorkTeamId { get; set; }
    }
}
