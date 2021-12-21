using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class TrackingConcernNote
    {
        public TrackingConcernNote()
        {
            StaffInvolved = new HashSet<TrackingConcernStaff>();
            ManagerInvolved = new HashSet<TrackingConcernManager>();
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
        public string Remarks { get; set; }
        public string Attachment { get; set; }

        public virtual ICollection<TrackingConcernStaff> StaffInvolved { get; set; }
        public virtual ICollection<TrackingConcernManager> ManagerInvolved { get; set; }

    }
}
