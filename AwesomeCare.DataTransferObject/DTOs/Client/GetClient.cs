using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using AwesomeCare.DataTransferObject.DTOs.Pets;
using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
using AwesomeCare.DataTransferObject.DTOs.HospitalExit;

namespace AwesomeCare.DataTransferObject.DTOs.Client
{
   public class GetClient
    {
        public GetClient()
        {
            InvolvingParties = new List<GetClientInvolvingPartyForEdit>();
            RegulatoryContact = new List<GetClientRegulatoryContactForEdit>();
            GetClientComplain = new List<GetClientComplainRegister>();
            GetClientLogAudit = new List<GetClientLogAudit>();
            GetClientMedAudit = new List<GetClientMedAudit>();
            GetClientVoice = new HashSet<GetClientVoice>();
            GetClientMgtVisit = new HashSet<GetClientMgtVisit>();
            GetClientProgram = new HashSet<GetClientProgram>();
            GetClientServiceWatch = new HashSet<GetClientServiceWatch>();

            GetClientBloodCoagulationRecord = new HashSet<GetClientBloodCoagulationRecord>();
            GetClientBMIChart = new HashSet<GetClientBMIChart>();
            GetClientBloodPressure = new HashSet<GetClientBloodPressure>();
            GetClientBodyTemp = new HashSet<GetClientBodyTemp>();
            GetClientBowelMovement = new HashSet<GetClientBowelMovement>();
            GetClientEyeHealthMonitoring = new HashSet<GetClientEyeHealthMonitoring>();
            GetClientFoodIntake = new HashSet<GetClientFoodIntake>();
            GetClientOxygenLvl = new HashSet<GetClientOxygenLvl>();
            GetClientPainChart = new HashSet<GetClientPainChart>();
            GetClientHeartRate = new HashSet<GetClientHeartRate>();
            GetClientPulseRate = new HashSet<GetClientPulseRate>();
            GetClientSeizure = new HashSet<GetClientSeizure>();
            GetClientWoundCare = new HashSet<GetClientWoundCare>();

            GetHealthAndLiving = new HashSet<GetHealthAndLiving>();
            GetBalance = new HashSet<GetBalance>();
            GetHistoryOfFall = new HashSet<GetHistoryOfFall>();
            GetPhysicalAbility = new HashSet<GetPhysicalAbility>();
            GetSpecialHealthAndMedication = new HashSet<GetSpecialHealthAndMedication>();
            GetSpecialHealthCondition = new HashSet<GetSpecialHealthCondition>();
            GetCarePlanNutrition = new HashSet<GetCarePlanNutrition>();
            GetInfectionControl = new HashSet<GetInfectionControl>();
            GetManagingTasks = new HashSet<GetManagingTasks>();
            GetPersonalHygiene = new HashSet<GetPersonalHygiene>();
            GetInterestAndObjective = new HashSet<GetInterestAndObjective>();
            GetReview = new HashSet<GetReview>();
            GetPets = new HashSet<GetPets>();
            GetBaseRecords = new HashSet<GetBaseRecordItem>();
            GetHospitalEntry = new HashSet<GetHospitalEntry>();
            GetHospitalExit = new HashSet<GetHospitalExit>();
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

        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Number of Calls")]
        public int NumberOfCalls { get; set; }

        [Display(Name = "Area Code")]
        public int AreaCodeId { get; set; }

        [Display(Name = "Teritory")]
        public int TeritoryId { get; set; }

        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        [Display(Name = "Provision Venue")]
        public string ProvisionVenue { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

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

        public int StatusId { get; set; }
        public string Status { get; set; }
        [Display(Name = "Capacity")]
        public int CapacityId { get; set; }

        public string ProviderReference { get; set; }

        [Display(Name = "Number of Staff")]
        public int NumberOfStaff { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public byte[] QRCode { get; set; }
        public string PassportFilePath { get; set; }

        public virtual ICollection<GetClientComplainRegister> GetClientComplain { get; set; }
        public virtual ICollection<GetClientInvolvingPartyForEdit> InvolvingParties { get; set; }
        public virtual ICollection<GetClientRegulatoryContactForEdit> RegulatoryContact { get; set; }
        public virtual ICollection<GetClientLogAudit> GetClientLogAudit { get; set; }
        public virtual ICollection<GetClientMedAudit> GetClientMedAudit { get; set; }
        public virtual ICollection<GetClientVoice> GetClientVoice { get; set; }
        public virtual ICollection<GetClientMgtVisit> GetClientMgtVisit { get; set; }
        public virtual ICollection<GetClientProgram> GetClientProgram { get; set; }
        public virtual ICollection<GetClientServiceWatch> GetClientServiceWatch { get; set; }

        public virtual ICollection<GetClientBloodCoagulationRecord> GetClientBloodCoagulationRecord { get; set; }
        public virtual ICollection<GetClientBMIChart> GetClientBMIChart { get; set; }
        public virtual ICollection<GetClientBloodPressure> GetClientBloodPressure { get; set; }
        public virtual ICollection<GetClientBodyTemp> GetClientBodyTemp{ get; set; }
        public virtual ICollection<GetClientBowelMovement> GetClientBowelMovement { get; set; }
        public virtual ICollection<GetClientEyeHealthMonitoring> GetClientEyeHealthMonitoring { get; set; }
        public virtual ICollection<GetClientFoodIntake> GetClientFoodIntake { get; set; }
        public virtual ICollection<GetClientHeartRate> GetClientHeartRate { get; set; }
        public virtual ICollection<GetClientOxygenLvl> GetClientOxygenLvl { get; set; }
        public virtual ICollection<GetClientPainChart> GetClientPainChart { get; set; }
        public virtual ICollection<GetClientPulseRate> GetClientPulseRate { get; set; }
        public virtual ICollection<GetClientSeizure> GetClientSeizure { get; set; }
        public virtual ICollection<GetClientWoundCare> GetClientWoundCare { get; set; }

        public virtual ICollection<GetHealthAndLiving> GetHealthAndLiving { get; set; }
        public virtual ICollection<GetBalance> GetBalance { get; set; }
        public virtual ICollection<GetHistoryOfFall> GetHistoryOfFall { get; set; }
        public virtual ICollection<GetPhysicalAbility> GetPhysicalAbility { get; set; }
        public virtual ICollection<GetSpecialHealthAndMedication> GetSpecialHealthAndMedication { get; set; }
        public virtual ICollection<GetSpecialHealthCondition> GetSpecialHealthCondition { get; set; }

        public virtual ICollection<GetCarePlanNutrition> GetCarePlanNutrition { get; set; }

        public virtual ICollection<GetInfectionControl> GetInfectionControl { get; set; }

        public virtual ICollection<GetReview> GetReview { get; set; }

        public virtual ICollection<GetManagingTasks> GetManagingTasks { get; set; }
        public virtual ICollection<GetPersonalHygiene> GetPersonalHygiene { get; set; }

        public virtual ICollection<GetInterestAndObjective> GetInterestAndObjective { get; set; }
        public virtual ICollection<GetPets> GetPets { get; set; }

        public virtual ICollection<GetHospitalEntry> GetHospitalEntry { get; set; }

        public virtual ICollection<GetBaseRecordItem> GetBaseRecords { get; set; }
        public virtual ICollection<GetHospitalExit> GetHospitalExit { get; set; }
    }
}
