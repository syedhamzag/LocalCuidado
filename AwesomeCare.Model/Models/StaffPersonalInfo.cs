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

        public virtual StaffWorkTeam StaffWorkTeam { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
