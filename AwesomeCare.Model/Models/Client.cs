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
            StaffSpotCheck = new HashSet<StaffSpotCheck>();
            StaffAdlObs = new HashSet<StaffAdlObs>();
            StaffMedCompObs = new HashSet<StaffMedComp>();
            StaffKeyWorkerVoice = new HashSet<StaffKeyWorkerVoice>();
            StaffReference = new HashSet<StaffReference>();
            Enotice = new HashSet<Enotice>();
            Resources = new HashSet<Resources>();
            IncomingMeds = new HashSet<IncomingMeds>();
            WhisttleBlower = new HashSet<WhisttleBlower>();
            ClientBloodPressure = new HashSet<ClientBloodPressure>();
            ClientFoodIntake = new HashSet<ClientFoodIntake>();
            ClientBowelMovement = new HashSet<ClientBowelMovement>();
            ClientPainChart = new HashSet<ClientPainChart>();
            ClientWoundCare = new HashSet<ClientWoundCare>();
            ClientSeizure = new HashSet<ClientSeizure>();
            ClientBloodCoagulationRecord = new HashSet<ClientBloodCoagulationRecord>();
            ClientEyeHealthMonitoring = new HashSet<ClientEyeHealthMonitoring>();
            ClientHeartRate = new HashSet<ClientHeartRate>();
            ClientPulseRate = new HashSet<ClientPulseRate>();
            ClientBodyTemp = new HashSet<ClientBodyTemp>();
            ClientOxygenLvl = new HashSet<ClientOxygenLvl>();
            ClientBMIChart = new HashSet<ClientBMIChart>(); 
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
        public virtual ICollection<StaffSpotCheck> StaffSpotCheck { get; set; }
        public virtual ICollection<StaffAdlObs> StaffAdlObs { get; set; }
        public virtual ICollection<StaffMedComp> StaffMedCompObs { get; set; }
        public virtual ICollection<StaffKeyWorkerVoice> StaffKeyWorkerVoice { get; set; }
        public virtual ICollection<StaffReference> StaffReference { get; set; }

        public virtual ICollection<Enotice> Enotice { get; set; }
        public virtual ICollection<Resources> Resources { get; set; }
        public virtual ICollection<IncomingMeds> IncomingMeds { get; set; }
        public virtual ICollection<WhisttleBlower> WhisttleBlower { get; set; }
        public virtual ICollection<ClientBloodPressure> ClientBloodPressure { get; set; }
        public virtual ICollection<ClientFoodIntake> ClientFoodIntake { get; set; }
        public virtual ICollection<ClientBowelMovement> ClientBowelMovement { get; set; }
        public virtual ICollection<ClientPainChart> ClientPainChart { get; set; }
        public virtual ICollection<ClientWoundCare> ClientWoundCare { get; set; }
        public virtual ICollection<ClientSeizure> ClientSeizure { get; set; }
        public virtual ICollection<ClientBloodCoagulationRecord> ClientBloodCoagulationRecord { get; set; }
        public virtual ICollection<ClientEyeHealthMonitoring> ClientEyeHealthMonitoring { get; set; }
        public virtual ICollection<ClientHeartRate> ClientHeartRate { get; set; }
        public virtual ICollection<ClientPulseRate> ClientPulseRate { get; set; }
        public virtual ICollection<ClientBodyTemp> ClientBodyTemp { get; set; }
        public virtual ICollection<ClientOxygenLvl> ClientOxygenLvl { get; set; }
        public virtual ICollection<ClientBMIChart> ClientBMIChart { get; set; }

    }
}
