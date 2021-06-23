using AwesomeCare.DataTransferObject.DTOs.ClientAdlObs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffAdlObs
{
    public class PutStaffAdlObs
    {
        public PutStaffAdlObs()
        {
            OfficerToAct = new List<PutAdlObsOfficerToAct>();
        }

        public int ObservationID {get; set;}
        public string Reference { get; set; }
        public int StaffId {get; set;}
        public DateTime Date{get; set;}
        public DateTime NextCheckDate{get; set;}
        public string Details {get; set;}
        public int ClientId {get; set;}
        public int UnderstandingofEquipment {get; set;}
        public int UnderstandingofService {get; set;}
        public int UnderstandingofControl {get; set;}
        public int FivePrinciples {get; set;}
        public string Comments {get; set;}
        public string ActionRequired {get; set;}
        public DateTime Deadline{get; set;}
        public int Status {get; set;}
        public string Remarks {get; set;}
        public string URL {get; set;}
        public string Attachment { get; set; }

        public List<PutAdlObsOfficerToAct> OfficerToAct { get; set; }
    }
}
