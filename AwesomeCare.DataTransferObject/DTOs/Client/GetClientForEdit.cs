﻿using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Client
{
   public class GetClientForEdit
    {
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile ClientImage { get; set; }
        public int ClientId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public string PreferredName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string About { get; set; }
        public string Hobbies { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        [Display(Name ="Gender")]
        public int GenderId { get; set; }
        [Display(Name = "Number of Calls")]
        public int NumberOfCalls { get; set; }
        [Display(Name = "Area Code")]
        public int AreaCodeId { get; set; }
        [Display(Name = "Teritory")]
        public int TeritoryId { get; set; }
        [Display(Name ="Service")]
        public int ServiceId { get; set; }
        [Display(Name = "Provision Venue")]
        public string ProvisionVenue { get; set; }
        public string PostCode { get; set; }
        public decimal Rate { get; set; }
        public string TeamLeader { get; set; }
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        public string KeySafe { get; set; }
        [Display(Name = "Choice of Staff")]
        public int ChoiceOfStaffId { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Display(Name = "Capacity")]
        public int CapacityId { get; set; }
        [Display(Name = "Provider Reference")]
        public string ProviderReference { get; set; }
        [Display(Name = "Number of Staff")]
        public int NumberOfStaff { get; set; }
        public string UniqueId { get; set; }
        public string PassportFilePath { get; set; }

        public int InvolvingPartyCount { get; set; }
        public int ClientManager { get; set; }
        public int Denture { get; set; }
        public int Aid { get; set; }
        public int TeamLeaderId { get; set; }
        public int KeyworkerId { get; set; }
        [Required]
        [MaxLength(4)]
        public string Pin { get; set; }

        [Display(Name = "Clock In/Out Distance")]
        public double LocationDistance { get; set; }
        public virtual ICollection<GetClientInvolvingPartyForEdit> InvolvingParties { get; set; }
        public virtual ICollection<GetClientRegulatoryContactForEdit> RegulatoryContact { get; set; }
    }
}
