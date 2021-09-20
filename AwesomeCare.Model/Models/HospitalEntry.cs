using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HospitalEntry
    {
        public HospitalEntry()
        {
            StaffInvolved = new HashSet<HospitalEntryStaffInvolved>();
            PersonToTakeAction = new HashSet<HospitalEntryPersonToTakeAction>();
        }

        public int HospitalEntryId { get; set; }
        public int ClientId { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string PurposeofAdmission { get; set; }
        public string CauseofAdmission { get; set; }
        public DateTime LastDateofAdmission { get; set; }
        public int ConditionOfAdmission { get; set; }
        public int IsFamilyInformed { get; set; }
        public DateTime PossibleDateReturn { get; set; }
        public int IsHomeCleaned { get; set; }
        public int NameParamedicStaff { get; set; }
        public int ParamicStaffTeamNo { get; set; }
        public string URLLINK  { get; set; }
        public int MeansOfTransport { get; set; }
        public string Attachment { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<HospitalEntryStaffInvolved> StaffInvolved { get; set; }
        public virtual ICollection<HospitalEntryPersonToTakeAction> PersonToTakeAction { get; set; }
    }
}
