using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class Client
    {
        public Client()
        {
            InvolvingParties = new HashSet<ClientInvolvingParty>();
            ClientCareDetails = new HashSet<ClientCareDetails>();
            RegulatoryContact = new HashSet<ClientRegulatoryContact>();
            ClientRota = new HashSet<ClientRota>();
            StaffBlackList = new HashSet<StaffBlackList>();
            ComplainRegister = new HashSet<ClientComplainRegister>();
            ClientNutrition = new HashSet<ClientNutrition>();
            ClientLogAudit = new HashSet<ClientLogAudit>();
            ClientMedAudit = new HashSet<ClientMedAudit>();
            ClientVoice = new HashSet<ClientVoice>();
            ClientMgtVisit = new HashSet<ClientMgtVisit>();
            ClientProgram = new HashSet<ClientProgram>();
            ClientServiceWatch = new HashSet<ClientServiceWatch>();
        }
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
        public string UniqueId  { get; set; }
        public string PassportFilePath { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual ICollection<ClientInvolvingParty> InvolvingParties { get; set; }
        public virtual ICollection<ClientRegulatoryContact> RegulatoryContact { get; set; }
        public virtual ICollection<ClientRota> ClientRota { get; set; }
        public virtual ICollection<ClientCareDetails> ClientCareDetails { get; set; }
        public virtual ICollection<ClientMedication> ClientMedication { get; set; }
        public virtual ICollection<StaffBlackList> StaffBlackList { get; set; }
        public virtual ICollection<ClientComplainRegister> ComplainRegister { get; set; }
        public virtual ICollection<ClientNutrition> ClientNutrition { get; set; }
        public virtual ICollection<ClientLogAudit> ClientLogAudit { get; set; }
        public virtual ICollection<ClientMedAudit> ClientMedAudit { get; set; }
        public virtual ICollection<ClientVoice> ClientVoice { get; set; }
        public virtual ICollection<ClientMgtVisit> ClientMgtVisit { get; set; }
        public virtual ICollection<ClientProgram> ClientProgram { get; set; }
        public virtual ICollection<ClientServiceWatch> ClientServiceWatch { get; set; }

    }
}
