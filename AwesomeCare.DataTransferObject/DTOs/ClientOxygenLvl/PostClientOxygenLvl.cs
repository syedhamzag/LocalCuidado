using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl
{
    public class PostClientOxygenLvl
    {
        public PostClientOxygenLvl()
        {
            OfficerToAct = new List<PostOxygenLvlOfficerToAct>();
            Physician = new List<PostOxygenLvlPhysician>();
            StaffName = new List<PostOxygenLvlStaffName>();
        }

        public int OxygenLvlId { get; set; }
        public int ClientId { get; set; }
        public string Reference { get; set; }
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

        public List<PostOxygenLvlOfficerToAct> OfficerToAct { get; set; }
        public List<PostOxygenLvlPhysician> Physician { get; set; }
        public List<PostOxygenLvlStaffName> StaffName { get; set; }
    }
}
