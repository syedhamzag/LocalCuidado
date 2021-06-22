using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientBodyTemp
    {
        public ClientBodyTemp()
        {
            Physician = new HashSet<BodyTempPhysician>();
            StaffName = new HashSet<BodyTempStaffName>();
            OfficerToAct = new HashSet<BodyTempOfficerToAct>();
        }
        public int BodyTempId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetTemp { get; set; }
        public string TargetTempAttach { get; set; }
        public string CurrentReading { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    
    public virtual Client Client { get; set; }
        public virtual ICollection<BodyTempOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<BodyTempStaffName> StaffName { get; set; }
        public virtual ICollection<BodyTempPhysician> Physician { get; set; }
    }

}
