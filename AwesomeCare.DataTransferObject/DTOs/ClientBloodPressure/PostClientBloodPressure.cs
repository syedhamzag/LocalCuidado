using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure
{
    public class PostClientBloodPressure
    {
        public PostClientBloodPressure()
        {
            OfficerToAct = new List<PostBloodPressureOfficerToAct>();
            Physician = new List<PostBloodPressurePhysician>();
            StaffName = new List<PostBloodPressureStaffName>();
        }
        public string Reference { get; set; }
        public int BloodPressureId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int GoalSystolic { get; set; }
        public int GoalDiastolic { get; set; }
        public int ReadingSystolic { get; set; }
        public int ReadingDiastolic { get; set; }
        public int StatusImage { get; set; }
        public string Comment { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

        public List<PostBloodPressureOfficerToAct> OfficerToAct { get; set; }
        public List<PostBloodPressurePhysician> Physician { get; set; }
        public List<PostBloodPressureStaffName> StaffName { get; set; }
    }
}
