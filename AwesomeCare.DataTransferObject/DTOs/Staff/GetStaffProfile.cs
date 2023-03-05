using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
using AwesomeCare.DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervision;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
using AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryAllowance;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryDeduction;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax;
using AwesomeCare.DataTransferObject.DTOs.StaffOfficeLocation;
using AwesomeCare.DataTransferObject.DTOs.Staff.Lateness;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
    public class GetStaffProfile
    {
        public GetStaffProfile()
        {
            Education = new List<GetStaffEducation>();
            Trainings = new List<GetStaffTraining>();
            References = new List<GetStaffReferee>();
            RegulatoryContacts = new List<GetStaffRegulatoryContact>();
            EmergencyContacts = new List<GetStaffEmergencyContact>();
            GetStaffSpotCheck = new List<GetStaffSpotCheck>();
            GetStaffAdlObs = new List<GetStaffAdlObs>();
            GetStaffMedComp = new List<GetStaffMedComp>();
            GetStaffKeyWorkerVoice = new List<GetStaffKeyWorkerVoice>();
            GetStaffSurvey = new List<GetStaffSurvey>();
            GetStaffSupervisionAppraisal = new List<GetStaffSupervisionAppraisal>();
            GetStaffOneToOne = new List<GetStaffOneToOne>();
            GetStaffReference = new List<GetStaffReference>();
            GetStaffPersonalityTest = new List<GetStaffPersonalityTest>();
            GetStaffInfectionControl = new List<GetStaffInfectionControl>();
            GetStaffCompetenceTest = new List<GetStaffCompetenceTest>();
            GetStaffHealth = new List<GetStaffHealth>();
            GetStaffInterview  = new List<GetStaffInterview>();
            GetStaffShadowing = new List<GetStaffShadowing>();
            GetStaffHoliday = new List<GetStaffHoliday>();
            GetStaffTrainingMatrix = new List<GetStaffTrainingMatrix>();
            GetSalaryAllowance = new List<GetSalaryAllowance>();
            GetSalaryDeduction = new List<GetSalaryDeduction>();
            GetStaffTax = new List<GetStaffTax>();
    }
        public List<GetStaffEducation> Education { get; set; }
        public List<GetStaffTraining> Trainings { get; set; }
        public List<GetStaffReferee> References { get; set; }
        public List<GetStaffRegulatoryContact> RegulatoryContacts { get; set; }
        public List<GetStaffEmergencyContact> EmergencyContacts { get; set; }
        public List<GetStaffSpotCheck> GetStaffSpotCheck { get; set; }
        public List<GetStaffAdlObs> GetStaffAdlObs { get; set; }
        public List<GetStaffMedComp> GetStaffMedComp { get; set; }
        public List<GetStaffKeyWorkerVoice> GetStaffKeyWorkerVoice { get; set; }
        public List<GetStaffSurvey> GetStaffSurvey { get; set; }
        public List<GetStaffSupervisionAppraisal> GetStaffSupervisionAppraisal { get; set; }
        public List<GetStaffOneToOne> GetStaffOneToOne { get; set; }
        public List<GetStaffReference> GetStaffReference { get; set; }
        public List<GetStaffPersonalityTest> GetStaffPersonalityTest { get; set; }
        public List<GetStaffInfectionControl> GetStaffInfectionControl { get; set; }
        public List<GetStaffCompetenceTest> GetStaffCompetenceTest { get; set; }
        public List<GetStaffHealth> GetStaffHealth { get; set; }
        public List<GetStaffInterview> GetStaffInterview{ get; set; }
        public List<GetStaffShadowing> GetStaffShadowing { get; set; }
        public List<GetStaffHoliday> GetStaffHoliday { get; set; }
        public List<GetStaffTrainingMatrix> GetStaffTrainingMatrix { get; set; }
        public List<GetSalaryAllowance> GetSalaryAllowance { get; set; }
        public List<GetSalaryDeduction> GetSalaryDeduction { get; set; }
        public List<GetStaffTax> GetStaffTax { get; set; }
        public List<GetStaffOfficeLocation> GetStaffOfficeLocation { get; set; } = new List<GetStaffOfficeLocation>();
        public List<GetStaffLateness> GetStaffLateness { get; set; } = new List<GetStaffLateness>();
        public int StaffPersonalInfoId { get; set; }
        public string ApplicationUserId { get; set; }
        [Display(Name ="Staff Number")]
        public string RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        [Display(Name = "Profile Pix")]
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
        [Display(Name = "Can Drive?")]
        public string CanDrive { get; set; }
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }
        [Display(Name = "Driving License ExpiryDate")]
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        [Display(Name = "Right To Work?")]
        public string RightToWork { get; set; }
        [Display(Name = "Right To Work Attachment")]
        public string RightToWorkAttachment { get; set; }
        [Display(Name = "Right To Work ExpiryDate")]
        public DateTime? RightToWorkExpiryDate { get; set; }
        public string DBS { get; set; }
        [Display(Name = "DBS Attachment")]
        public string DBSAttachment { get; set; }
        [Display(Name = "DBS ExpiryDate")]
        public DateTime? DBSExpiryDate { get; set; }
        [Display(Name = "DBS UpdateNo")]
        public string DBSUpdateNo { get; set; }
        public string NI { get; set; }
        [Display(Name = "NI Attachment")]
        public string NIAttachment { get; set; }
        [Display(Name = "NI ExpiryDate")]
        public DateTime? NIExpiryDate { get; set; }

        public string CV { get; set; }
        public string CoverLetter { get; set; }
        [Display(Name = "Self/PYE")]
        public string Self_PYE { get; set; }
        [Display(Name = "Self/PYE Attachment")]
        public string Self_PYEAttachment { get; set; }
        public StaffRegistrationEnum Status { get; set; }
        [Display(Name = "Job Category")]
        public int? JobCategory { get; set; }
        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

       
        [Display(Name = "Is Team Leader?")]
        public string IsTeamLeader { get; set; }
       
        [Display(Name = "Has Uniform?")]
        public string HasUniform { get; set; }
       
        [Display(Name = "Has Id Card?")]
        public string HasIdCard { get; set; }
       
        [Display(Name = "Employment Date")]
        public DateTime? EmploymentDate { get; set; }
    }
}
