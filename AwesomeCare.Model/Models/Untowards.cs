using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class Untowards
    {
        public Untowards()
        {
            StaffInvolved = new HashSet<UntowardsStaffInvolved>();
            OfficerToAct = new HashSet<UntowardsOfficerToAct>();
        }
        public int UntowardsId { get; set; }
        public string TicketNumber { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string TimeOfCall { get; set; }
        public string PersonReporting { get; set; }
        public string PersonReportingTelephone { get; set; }
        public string PersonReportingEmail { get; set; }
        public string Details { get; set; }
        public string ActionStatus { get; set; }
        public string Priority { get; set; }
        public string ActionTaken { get; set; }
        public string ActionRequired { get; set; }
        public string FinalActionToCloseCase { get; set; }
        public string ExpectedDateAndTimeOfFeedback { get; set; }
        public bool IsBlackListRequired { get; set; }
        public int HomeCareClientId { get; set; }
        public bool IsHospitalEntry { get; set; }
        public string HospitalEntryReason { get; set; }
        public bool IsHospitalExit { get; set; }
        public string HospitalExitDetails { get; set; }
        /// <summary>
        /// Client Involving Party BaseRecord Item
        /// </summary>
        public int TypeofRequiredNotification { get; set; }
        public bool ShouldNotifyInvolvingStaff { get; set; }
        public string Attachment { get; set; }
        public string Others { get; set; }
        public string EntryHospitalName { get; set; }
        public string ExitHospitalName { get; set; }

        public virtual ICollection<UntowardsStaffInvolved> StaffInvolved { get; set; }
        public virtual ICollection<UntowardsOfficerToAct> OfficerToAct { get; set; }
    }
}
