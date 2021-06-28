using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffMedComp
{
    public class GetStaffMedComp
    {
        public GetStaffMedComp()
        {
            OfficerToAct = new List<GetMedCompOfficerToAct>();
        }
        public int MedCompId {get; set;}
        public string Reference { get; set; }
        public int StaffId {get; set;}
        public DateTime Date {get; set;}
        public DateTime NextCheckDate {get; set;}
        public string Details {get; set;}
        public int ClientId {get; set;}
        public int UnderstandingofMedication {get; set;}
        public int UnderstandingofRights {get; set;}
        public int ReadingMedicalPrescriptions {get; set;}
        public int CarePlan {get; set;}
        public int RateStaff {get; set;}
        public string ActionRequired {get; set;}
        public DateTime Deadline {get; set;}
        public int Status {get; set;}
        public string Remarks {get; set;}
        public string URL {get; set;}
        public string Attachment { get; set; }

        public List<GetMedCompOfficerToAct> OfficerToAct { get; set; }
    }
}
