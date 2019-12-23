using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
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
        public string Email { get; set; }
        public string About { get; set; }
        public string Hobbies { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        [Display(Name ="Gender")]
        public int GenderId { get; set; }
        public int NumberOfCalls { get; set; }
        public int AreaCodeId { get; set; }
        public int TeritoryId { get; set; }
        public int ServiceId { get; set; }
        public string ProvisionVenue { get; set; }
        public string PostCode { get; set; }
        public decimal Rate { get; set; }
        public string TeamLeader { get; set; }
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public int LanguageId { get; set; }
        public string KeySafe { get; set; }
        public int ChoiceOfStaffId { get; set; }
        public int StatusId { get; set; }
        public int CapacityId { get; set; }
        public string ProviderReference { get; set; }
        public int NumberOfStaff { get; set; }
        public string UniqueId { get; set; }
        public virtual ICollection<GetClientInvolvingPartyForEdit> InvolvingParties { get; set; }
        public virtual ICollection<GetClientRegulatoryContactForEdit> RegulatoryContact { get; set; }
    }
}
