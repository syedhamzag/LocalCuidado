using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.HospitalEntry
{
     public class PutHospitalEntry
    {
        public PutHospitalEntry()
        {
            StaffInvolved = new List<PutHospitalEntryStaffInvolved>();
            PersonToTakeAction = new List<PutHospitalEntryPersonToTakeAction>();
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
        public string URLLINK { get; set; }
        public int MeansOfTransport { get; set; }
        public string Attachment { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public List<PutHospitalEntryStaffInvolved> StaffInvolved { get; set; }
        public List<PutHospitalEntryPersonToTakeAction> PersonToTakeAction { get; set; }
    }
}
