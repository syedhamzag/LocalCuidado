using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHeartRate
{
    public class PostClientHeartRate
    {
        public PostClientHeartRate()
        {
            OfficerToAct = new List<PostHeartRateOfficerToAct>();
            Physician = new List<PostHeartRatePhysician>();
            StaffName = new List<PostHeartRateStaffName>();
        }

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

        public List<PostHeartRateOfficerToAct> OfficerToAct { get; set; }
        public List<PostHeartRatePhysician> Physician { get; set; }
        public List<PostHeartRateStaffName> StaffName { get; set; }
    }
}
