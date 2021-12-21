using AwesomeCare.DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffPersonalInfo
    {
        public StaffPersonalInfo()
        {
            Education = new HashSet<StaffEducation>();
            Trainings = new HashSet<StaffTraining>();
            References = new HashSet<StaffReferee>();
            StaffPersonalInfoComments = new HashSet<StaffPersonalInfoComment>();
            RegulatoryContact = new HashSet<StaffRegulatoryContact>();
            EmergencyContacts = new HashSet<StaffEmergencyContact>();
            ShiftBookings = new HashSet<StaffShiftBooking>();
            StaffRating = new HashSet<StaffRating>();
            StaffBlackList = new HashSet<StaffBlackList>();
            ClientNutrition = new HashSet<ClientNutrition>();
            ClientLogAudit = new HashSet<ClientLogAudit>();
            ClientMedAudit = new HashSet<ClientMedAudit>();
            ClientVoice = new HashSet<ClientVoice>();
            ClientShopping = new HashSet<ClientShopping>();
            ClientCleaning = new HashSet<ClientCleaning>();
            ClientMgtVisit = new HashSet<ClientMgtVisit>();
            ClientProgram = new HashSet<ClientProgram>();
            ClientServiceWatch = new HashSet<ClientServiceWatch>();
            StaffSpotCheck = new HashSet<StaffSpotCheck>();
            StaffAdlObs = new HashSet<StaffAdlObs>();
            StaffMedCompObs = new HashSet<StaffMedComp>();
            StaffKeyWorkerVoice = new HashSet<StaffKeyWorkerVoice>();
            StaffSurvey = new HashSet<StaffSurvey>();
            StaffOneToOne = new HashSet<StaffOneToOne>();
            StaffSupervisionAppraisal = new HashSet<StaffSupervisionAppraisal>();
            StaffReference = new HashSet<StaffReference>();
            Equipment = new HashSet<Equipment>();
            HospitalEntryPersonToTakeAction = new HashSet<HospitalEntryPersonToTakeAction>();
            HospitalEntryStaffInvolved = new HashSet<HospitalEntryStaffInvolved>();
            HospitalExitOfficerToTakeAction = new HashSet<HospitalExitOfficerToTakeAction>();
            StaffPersonalityTest = new HashSet<StaffPersonalityTest>();
            StaffInfectionControl = new HashSet<StaffInfectionControl>();
            StaffCompetenceTest = new HashSet<StaffCompetenceTest>();
            StaffInterview = new HashSet<StaffInterview>();
            StaffHealth = new HashSet<StaffHealth>();
            StaffShadowing = new HashSet<StaffShadowing>();
            StaffHoliday = new HashSet<StaffHoliday>();
            SetupStaffHoliday = new HashSet<SetupStaffHoliday>();
            StaffTeamLead = new HashSet<StaffTeamLead>();
            StaffTrainingMatrix = new HashSet<StaffTrainingMatrix>();
            FilesAndRecord = new HashSet<FilesAndRecord>();
            SalaryAllowance = new HashSet<SalaryAllowance>();
            SalaryDeduction = new HashSet<SalaryDeduction>();
        }

        public int StaffPersonalInfoId { get; set; }
        /// <summary>
        ///  ApplicationUser Id
        /// </summary>
        public string ApplicationUserId { get; set; }
        public string RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
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
        public int? StaffWorkTeamId { get; set; }
        public string Passcode { get; set; }
        public string CanDrive { get; set; }
        public string DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        public string RightToWork { get; set; }
        public string RightToWorkAttachment { get; set; }
        public DateTime? RightToWorkExpiryDate { get; set; }
        public string DBS { get; set; }
        public string DBSAttachment { get; set; }
        public DateTime? DBSExpiryDate { get; set; }
        public string DBSUpdateNo { get; set; }
        public string NI { get; set; }
        public string NIAttachment { get; set; }
        public DateTime? NIExpiryDate { get; set; }

        public string CV { get; set; }
        public string CoverLetter { get; set; }
        public string Self_PYE { get; set; }
        public string Self_PYEAttachment { get; set; }
        public StaffRegistrationEnum Status { get; set; }

        public bool? IsTeamLeader { get; set; }
        public bool? HasUniform { get; set; }
        public bool? HasIdCard { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int? JobCategory { get; set; }
        public string PlaceOfBirth { get; set; }

        public virtual ICollection<StaffEducation> Education { get; set; }
        public virtual ICollection<StaffTraining> Trainings { get; set; }
        public virtual ICollection<StaffReferee> References { get; set; }
        public virtual ICollection<StaffPersonalInfoComment> StaffPersonalInfoComments { get; set; }
        public virtual ICollection<StaffRegulatoryContact> RegulatoryContact { get; set; }
        public virtual ICollection<StaffEmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<StaffShiftBooking> ShiftBookings { get; set; }
        public virtual ICollection<StaffRating> StaffRating { get; set; }
        public virtual ICollection<StaffBlackList> StaffBlackList { get; set; }
        public virtual ICollection<ClientNutrition> ClientNutrition { get; set; }
        public virtual ICollection<ClientLogAudit> ClientLogAudit { get; set; }
        public virtual ICollection<ClientMedAudit> ClientMedAudit { get; set; }
        public virtual ICollection<ClientVoice> ClientVoice { get; set; }
        public virtual ICollection<ClientShopping> ClientShopping { get; set; }
        public virtual ICollection<ClientCleaning> ClientCleaning { get; set; }
        public virtual ICollection<ClientMgtVisit> ClientMgtVisit { get; set; }
        public virtual ICollection<ClientProgram> ClientProgram { get; set; }
        public virtual ICollection<ClientServiceWatch> ClientServiceWatch { get; set; }
        public virtual ICollection<StaffSpotCheck> StaffSpotCheck { get; set; }
        public virtual ICollection<StaffAdlObs> StaffAdlObs { get; set; }
        public virtual ICollection<StaffMedComp> StaffMedCompObs { get; set; }
        public virtual ICollection<StaffKeyWorkerVoice> StaffKeyWorkerVoice { get; set; }
        public virtual ICollection<StaffSurvey> StaffSurvey { get; set; }
        public virtual ICollection<StaffOneToOne> StaffOneToOne { get; set; }
        public virtual ICollection<StaffSupervisionAppraisal> StaffSupervisionAppraisal { get; set; }
        public virtual ICollection<StaffReference> StaffReference { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }

        public virtual ICollection<HospitalEntryStaffInvolved> HospitalEntryStaffInvolved { get; set; }
        public virtual ICollection<HospitalEntryPersonToTakeAction> HospitalEntryPersonToTakeAction { get; set; }
        public virtual ICollection<HospitalExitOfficerToTakeAction> HospitalExitOfficerToTakeAction { get; set; }
        public virtual ICollection<StaffPersonalityTest> StaffPersonalityTest { get; set; }
        public virtual ICollection<StaffCompetenceTest> StaffCompetenceTest { get; set; }
        public virtual ICollection<StaffInterview> StaffInterview { get; set; }
        public virtual ICollection<StaffHealth> StaffHealth { get; set; }
        public virtual ICollection<StaffShadowing> StaffShadowing { get; set; }

        public virtual StaffWorkTeam StaffWorkTeam { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<StaffInfectionControl> StaffInfectionControl { get; set; }
        public virtual ICollection<StaffHoliday> StaffHoliday { get; set; }
        public virtual ICollection<SetupStaffHoliday> SetupStaffHoliday { get; set; }
        public virtual ICollection<StaffTeamLead> StaffTeamLead { get; set; }
        public virtual ICollection<StaffTrainingMatrix> StaffTrainingMatrix { get; set; }
        public virtual ICollection<FilesAndRecord> FilesAndRecord { get; set; }
        public virtual ICollection<SalaryAllowance> SalaryAllowance { get; set; }
        public virtual ICollection<SalaryDeduction> SalaryDeduction { get; set; }

    }
}
 