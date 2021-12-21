using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure
{
    public class GetClientBloodPressure
    {
        public GetClientBloodPressure()
        {
            OfficerToAct = new List<GetBloodPressureOfficerToAct>();
            Physician = new List<GetBloodPressurePhysician>();
            StaffName = new List<GetBloodPressureStaffName>();
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

        public List<GetBloodPressureOfficerToAct> OfficerToAct { get; set; }
        public List<GetBloodPressurePhysician> Physician { get; set; }
        public List<GetBloodPressureStaffName> StaffName { get; set; }
    }
}
