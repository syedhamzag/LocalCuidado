using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientOxygenLvl
    {
        public ClientOxygenLvl()
        {
            Physician = new HashSet<OxygenLvlPhysician>();
            StaffName = new HashSet<OxygenLvlStaffName>();
            OfficerToAct = new HashSet<OxygenLvlOfficerToAct>();
        }
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
    
    public virtual Client Client { get; set; }
        public virtual ICollection<OxygenLvlOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<OxygenLvlStaffName> StaffName { get; set; }
        public virtual ICollection<OxygenLvlPhysician> Physician { get; set; }
    }

}
