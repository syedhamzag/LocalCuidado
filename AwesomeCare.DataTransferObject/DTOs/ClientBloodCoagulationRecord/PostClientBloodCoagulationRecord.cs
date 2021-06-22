using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord
{
    public class PostClientBloodCoagulationRecord
    {
        public PostClientBloodCoagulationRecord()
        {
            OfficerToAct = new List<PostBloodCoagOfficerToAct>();
            StaffName = new List<PostBloodCoagStaffName>();
            Physician = new List<PostBloodCoagPhysician>();
        }
        public int BloodRecordId { get; set; }
        public int ClientId { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Indication { get; set; }
        public int TargetINR { get; set; }
        public string TargetINRAttach { get; set; }
        public DateTime StartDate { get; set; }
        public int CurrentDose { get; set; }
        public int INR { get; set; }
        public int NewDose { get; set; }
        public int NewINR { get; set; }
        public int BloodStatus { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponce { get; set; }
        public DateTime Deadline { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }

        public List<PostBloodCoagOfficerToAct> OfficerToAct { get; set; }
        public List<PostBloodCoagStaffName> StaffName { get; set; }
        public List<PostBloodCoagPhysician> Physician { get; set; }
    }
}
