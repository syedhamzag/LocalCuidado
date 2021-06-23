using AwesomeCare.DataTransferObject.DTOs.ClientOneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffOneToOne
{
    public class PutStaffOneToOne
    {
        public PutStaffOneToOne()
        {
            OfficerToAct = new List<PutOneToOneOfficerToAct>();
        }
        public int OneToOneId { get; set; }
        public string Reference { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public string Purpose { get; set; }
        public int PreviousSupervision { get; set; }
        public string StaffImprovedInAreas { get; set; }
        public string CurrentEventArea { get; set; }
        public string StaffConclusion { get; set; }
        public string DecisionsReached { get; set; }
        public string ImprovementRecorded { get; set; }
        public string ActionRequired { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public List<PutOneToOneOfficerToAct> OfficerToAct { get; set; }
    }
}
