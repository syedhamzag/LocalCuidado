using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote
{
    public class ConcernNotes
    {
        public ConcernNotes()
        {
            StaffInvolved = new List<GetTrackingConcernStaff>();
            ManagerInvolved = new List<GetTrackingConcernManager>();
        }

        public int Ref { get; set; }
        public DateTime Date { get; set; }
        public string ConcernNote { get; set; }
        public string ActionRequired { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime ExpectedDeadline { get; set; }
        public int StaffNotify { get; set; }
        public int ManagerCopied { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }

        public virtual List<GetTrackingConcernStaff> StaffInvolved { get; set; }
        public virtual List<GetTrackingConcernManager> ManagerInvolved { get; set; }
    }
}
