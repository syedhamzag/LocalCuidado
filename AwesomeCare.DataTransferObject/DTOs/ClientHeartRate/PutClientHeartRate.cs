using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHeartRate
{
    public class PutClientHeartRate
    {
        public PutClientHeartRate()
        {
            OfficerToAct = new List<PutHeartRateOfficerToAct>();
            Physician = new List<PutHeartRatePhysician>();
            StaffName = new List<PutHeartRateStaffName>();
        }

        public List<PutHeartRateOfficerToAct> OfficerToAct { get; set; }
        public List<PutHeartRatePhysician> Physician { get; set; }
        public List<PutHeartRateStaffName> StaffName { get; set; }

        public int HeartRateId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetHR { get; set; }
        public string TargetHRAttach { get; set; }
        public int Gender { get; set; }
        public string GenderAttach { get; set; }
        public int Age { get; set; }
        public int BeatsPerSeconds { get; set; }
        public string Comment { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
