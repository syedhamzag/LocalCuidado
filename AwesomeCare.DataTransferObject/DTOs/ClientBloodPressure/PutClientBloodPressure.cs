using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure
{
    public class PutClientBloodPressure
    {
        public PutClientBloodPressure()
        {
            OfficerToAct = new List<PutBloodPressureOfficerToAct>();
            Physician = new List<PutBloodPressurePhysician>();
            StaffName = new List<PutBloodPressureStaffName>();
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

        public List<PutBloodPressureOfficerToAct> OfficerToAct { get; set; }
        public List<PutBloodPressurePhysician> Physician { get; set; }
        public List<PutBloodPressureStaffName> StaffName { get; set; }
    }
}
