using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaff
    {
       
        //public CreateStaff()
        //{
        //    Education = new List<CreateStaffEducation>();
        //    Education.Add(new CreateStaffEducation());

        //    Trainings = new List<CreateStaffTraining>();
        //    Trainings.Add(new CreateStaffTraining());

        //    References = new List<CreateStaffReference>();
        //    References.Add(new CreateStaffReference());
        //}
        public List<CreateStaffEducation> Education { get; set; }
        public List<CreateStaffTraining> Trainings { get; set; }
        public List<CreateStaffReference> References { get; set; }
        public List<CreateStaffRegulatoryContact> RegulatoryContacts { get; set; }
        public List<CreateStaffEmergencyContact> EmergencyContacts { get; set; }

        public string ApplicationUserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile ProfilePix { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        [Required]
        public string Telephone { get; set; }
        
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name = "About Me")]
        public string AboutMe { get; set; }
        [Required]
        public string Hobbies { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        
        [Required]
        public string PostCode { get; set; }
        [Display(Name = "Passcode/PIN max = 4 ")]
        [MaxLength(4)]
        public string Passcode { get; set; }
        public decimal? Rate { get; set; }
        public string TeamLeader { get; set; }
        public string WorkTeam { get; set; }
        [Required]
        [Display(Name ="Can you drive?")]
        public string CanDrive { get; set; }
        [Display(Name = "Attach Driving License if you can drive")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        [RequiredDependant("Yes",nameof(CanDrive),typeof(string),ErrorMessage ="Please attach Driving License if you can drive")]
        public IFormFile DrivingLicense { get; set; }
        [Display(Name = "Driving License ExpiryDate")]
        [RequiredDependant("Yes", nameof(CanDrive), typeof(string), ErrorMessage = "Please provide Driving License Expiry Date if you can drive")]
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        [Required]
        [Display(Name = "Right to work?")]
        public string RightToWork { get; set; }
        [Display(Name = "Right to work attachment")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        [RequiredDependant("Yes", nameof(RightToWork), typeof(string), ErrorMessage = "Please attach Right to Work")]
        public IFormFile RightToWorkAttachment { get; set; }
        [Display(Name = "Right to work attachment ExpiryDate")]
        [RequiredDependant("Yes", nameof(RightToWork), typeof(string), ErrorMessage = "Please provice Right to Work Expiry Date")]
        public DateTime? RightToWorkExpiryDate { get; set; }
        [Required]
        public string DBS { get; set; }
        [Display(Name = "DBS attachment")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please attach DBS")]
        public IFormFile DBSAttachment { get; set; }
        [Display(Name = "DBS ExpiryDate")]

        [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please provide DBS Expiry Date")]
        public DateTime? DBSExpiryDate { get; set; }
        [Display(Name = "DBS Update No")]
        [RequiredDependant("Yes", nameof(DBS), typeof(string), ErrorMessage = "Please provide DBS Update No.")]
        public string DBSUpdateNo { get; set; }
       
        [Display(Name = "NI")]
        public string NI { get; set; }
        [Display(Name = "NI attachment")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        [RequiredDependant("Yes", nameof(NI), typeof(string), ErrorMessage = "Please attach NI")]
        public IFormFile NIAttachment { get; set; }
        [Display(Name = "NI expiry date")]
        [RequiredDependant("Yes", nameof(NI), typeof(string), ErrorMessage = "Please provide NI Expiry Date")]
        public DateTime? NIExpiryDate { get; set; }
        [Required]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile CV { get; set; }
        [Required]
        [Display(Name = "Attach Cover Letter")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile CoverLetter { get; set; }
        [Display(Name = "Self PYE")]
        public string SelfPYE { get; set; }
        [Display(Name = "Self PYE attachment")]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        [RequiredDependant("Yes", nameof(SelfPYE), typeof(string), ErrorMessage = "Please attach Self PYE")]
        public IFormFile Self_PYEAttachment { get; set; }

        [Required]
        [Display(Name = "Job Category")]
        public int JobCategory { get; set; }
        [Required]
        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }
    }
}
