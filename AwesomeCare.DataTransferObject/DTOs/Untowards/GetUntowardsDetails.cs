using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.Untowards
{
   public class GetUntowardsDetails
    {
        public GetUntowardsDetails()
        {
            StaffInvolved = new List<GetUntowardsStaffInvolved>();
            OfficerToAct = new List<GetUntowardsOfficerToAct>();
        }
        public int UntowardsId { get; set; }
        [Display(Name = "Ticket Number")]
        public string TicketNumber { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Time Of Call")]
        public string TimeOfCall { get; set; }
        [Display(Name = "Person Reporting")]
        public string PersonReporting { get; set; }
        [Display(Name = "Person Reporting Telephone")]
        public string PersonReportingTelephone { get; set; }
        [Display(Name = "Person Reporting Email")]
        public string PersonReportingEmail { get; set; }
        public string Details { get; set; }
        [Display(Name = "Action Status")]
        public string ActionStatus { get; set; }
        public string Priority { get; set; }
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }
        [Display(Name = "Action Required")]
        public string ActionRequired { get; set; }
        [Display(Name = "Final Action To Close Case")]
        public string FinalActionToCloseCase { get; set; }
        [Display(Name = "Expected Date And Time Of Feedback")]
        public string ExpectedDateAndTimeOfFeedback { get; set; }
        [Display(Name = "Is BlackList Required?")]
        public string IsBlackListRequired { get; set; }
        [Display(Name = "Client")]
        public string HomeCareClient { get; set; }
        [Display(Name = "Is Hospital Entry?")]
        public string IsHospitalEntry { get; set; }
        [Display(Name = "Hospital Entry Reason")]
        public string HospitalEntryReason { get; set; }
        [Display(Name = "Is Hospital Exit?")]
        public string IsHospitalExit { get; set; }
        [Display(Name = "Hospital Exit Details")]
        public string HospitalExitDetails { get; set; }
        /// <summary>
        /// Client Involving Party BaseRecord Item
        /// </summary>
        [Display(Name = "Involving Party")]
        public string TypeofRequiredNotification { get; set; }
        [Display(Name = "Should Notify Involving Staff?")]
        public string ShouldNotifyInvolvingStaff { get; set; }
        public string Attachment { get; set; }
        public string Others { get; set; }
        [Display(Name = "Staffs Involved")]
        public List<GetUntowardsStaffInvolved> StaffInvolved { get; set; }
        [Display(Name = "Officer to act")]
        public List<GetUntowardsOfficerToAct> OfficerToAct { get; set; }
    }
}
