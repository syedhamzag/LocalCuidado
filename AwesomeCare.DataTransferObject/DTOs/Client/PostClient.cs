using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.Client
{
    public class PostClient
    {
        public PostClient()
        {
            InvolvingParties = new List<PostClientInvolvingParty>();
            RegulatoryContacts = new List<PostClientRegulatoryContact>();
            CareDetails = new List<PostClientCareDetails>();

        }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Middlename { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string PreferredName { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        public string About { get; set; }
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
        [Display(Name ="Gender")]
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
        [MaxLength(250)]
        public string Latitude { get; set; }
        [MaxLength(250)]
        public string Longitude { get; set; }
        public string PassportFilePath { get; set; }
        public int ClientManager { get; set; }
        public int Denture { get; set; }
        public int Aid { get; set; }

        public List<PostClientInvolvingParty> InvolvingParties { get; set; }
        public List<PostClientRegulatoryContact> RegulatoryContacts { get; set; }
        public List<PostClientCareDetails> CareDetails { get; set; }

    }
}
