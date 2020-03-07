using AwesomeCare.DataTransferObject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Untowards
{
   public class PostUntowards
    {
        public PostUntowards()
        {
            StaffInvolved = new List<PostUntowardsStaffInvolved>();
            OfficerToAct = new List<PostUntowardsOfficerToAct>();
        }
        
        [Required(ErrorMessage ="required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "required")]
        [MaxLength(225)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name ="Time of Call")]
        public string TimeOfCall { get; set; }
        [Required(ErrorMessage = "required")]
        [MaxLength(100)]
        [Display(Name = "Person Reporting")]
        public string PersonReporting { get; set; }
        [MaxLength(50)]
        [Display(Name = "Person Reporting Telephone")]
        public string PersonReportingTelephone { get; set; }
        [MaxLength(225)]
        [EmailAddress]
        [Display(Name = "Person Reporting Email")]
        public string PersonReportingEmail { get; set; }
        [MaxLength(225)]
        [Required(ErrorMessage = "required")]
        public string Details { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "Action Status")]
        public string ActionStatus { get; set; }
        [Required(ErrorMessage = "required")]
        public string Priority { get; set; }
        [MaxLength(225)]
        [Required(ErrorMessage = "required")]
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }
        [MaxLength(225)]
        [Required(ErrorMessage = "required")]
        [Display(Name = "Action Required")]
        public string ActionRequired { get; set; }
        [MaxLength(225)]
        [Display(Name = "Final Action To Close Case")]
        public string FinalActionToCloseCase { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "Expected Date And Time Of Feedback")]
        public string ExpectedDateAndTimeOfFeedback { get; set; }
        [Display(Name = "Is BlackList Required?")]
        public bool IsBlackListRequired { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "HomeCare Client")]
        public int HomeCareClientId { get; set; }
        [Display(Name = "Is Hospital Entry?")]
        public bool IsHospitalEntry { get; set; }
        [MaxLength(225)]
        [Display(Name = "Hospital Entry Reason")]
        [RequiredDependant("true", nameof(IsHospitalEntry), typeof(bool),ErrorMessage = "Hospital Entry Reason is required")]
        public string HospitalEntryReason { get; set; }
        [Display(Name = "Is Hospital Exit?")]
        public bool IsHospitalExit { get; set; }
        [MaxLength(225)]
        [Display(Name = "Hospital Exit Details")]
        [RequiredDependant("true", nameof(IsHospitalExit), typeof(bool),ErrorMessage = "Hospital Exit Details is required")]
        public string HospitalExitDetails { get; set; }
        /// <summary>
        /// Client Involving Party BaseRecord Item
        /// </summary>
        [Display(Name = "Type of Client Required Notification")]
        public int TypeofRequiredNotification { get; set; }
        [Display(Name = "Should Notify Involving Staff?")]
        public bool ShouldNotifyInvolvingStaff { get; set; }
        public string Attachment { get; set; }
        [MaxLength(225)]
        public string Others { get; set; }

        public List<PostUntowardsStaffInvolved> StaffInvolved { get; set; }
        public List<PostUntowardsOfficerToAct> OfficerToAct { get; set; }
    }
}
