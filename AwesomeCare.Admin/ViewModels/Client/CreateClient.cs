using AwesomeCare.DataTransferObject.Validations;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClient
    {
        
        public CreateClient()
        {
            Gender = new List<SelectListItem> {
                new SelectListItem("Male","Male"),
                new SelectListItem("Female","Female")
            };
            InvolvingParties = new List<ClientInvolvingParty>();
            RegulatoryContacts = new List<ClientRegulatoryContact>();
            CareDetails = new List<ClientCareDetailsHeading>();
            ComplainRegisters = new List<CreateComplainRegister>();
        }
        public int ClientId { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght =1)]
        [AllowedExtensions(new string[] { ".png", ".jpg" , ".jpeg" })]
        public IFormFile ClientImage { get; set; }
        #region DropDowns
        public IEnumerable<SelectListItem> Gender { get; set; }

        public bool CanContinue { get; set; }
        #endregion

        #region Tabs
        public string ActiveTab { get; set; } =  "personalInfo";
        // public string[] Tabs { get; set; } = new string[] { "" };
        #endregion

        #region PersonalInfo
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Middlename { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string About { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        public string Hobbies { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Keyworker { get; set; }
        [Required]
        [MaxLength(50)]
        public string IdNumber { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Required]
        [Display(Name = "Number of Calls")]
        public int NumberOfCalls { get; set; }
        [Required]
        [Display(Name = "Area Code")]
        public int AreaCodeId { get; set; }
        [Required]
        [Display(Name = "Teritory")]
        public int TeritoryId { get; set; }
        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Provision Venue")]
        public string ProvisionVenue { get; set; }
        [Required]
        [MaxLength(50)]
        public string PostCode { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Team Leader")]
        public string TeamLeader { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        [Required]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        [Required]
        [MaxLength(50)]
        public string KeySafe { get; set; }
        [Required]
        [Display(Name = "Choice Of Staff")]
        public int ChoiceOfStaffId { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Required]
        [Display(Name = "Capacity")]
        public int CapacityId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Provider Reference")]
        public string ProviderReference { get; set; }
        [Required]
        [Display(Name = "Number of Staff")]
        public int NumberOfStaff { get; set; }
        public string PassportFilePath { get; set; }
        #endregion
        public List<ClientInvolvingParty> InvolvingParties { get; set; }
        public List<ClientRegulatoryContact> RegulatoryContacts { get; set; }
        public List<ClientCareDetailsHeading> CareDetails { get; set; }
        public List<CreateComplainRegister> ComplainRegisters { get; set; }
        #region Methods
        public async Task SaveFileToDisk(IWebHostEnvironment env)
        {
            string filePath = GetFilePath(env);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await this.ClientImage.CopyToAsync(stream);
            }
        }
        public void DeleteFileFromDisk(IWebHostEnvironment env)
        {
            string filePath = GetFilePath(env);
            System.IO.File.Delete(filePath);
        }
        string GetFilePath(IWebHostEnvironment env)
        {
            string fileName = string.Concat(IdNumber, Path.GetExtension(ClientImage.FileName));
            string filePath = Path.Combine(env.ContentRootPath, "Uploads", fileName);
            return filePath;
        }
        #endregion
    }

}
