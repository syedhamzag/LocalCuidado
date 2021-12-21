using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl
{
    public class GetClientOxygenLvl
    {
        public GetClientOxygenLvl()
        {
            OfficerToAct = new List<GetOxygenLvlOfficerToAct>();
            Physician = new List<GetOxygenLvlPhysician>();
            StaffName = new List<GetOxygenLvlStaffName>();
        }

        public List<GetOxygenLvlOfficerToAct> OfficerToAct { get; set; }
        public List<GetOxygenLvlPhysician> Physician { get; set; }
        public List<GetOxygenLvlStaffName> StaffName { get; set; }

        public int OxygenLvlId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetOxygen { get; set; }
        public string TargetOxygenAttach { get; set; }
        public string CurrentReading { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
