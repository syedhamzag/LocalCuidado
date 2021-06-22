using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientEyeHealthMonitoring
    {
        public ClientEyeHealthMonitoring()
        {
            Physician = new HashSet<EyeHealthPhysician>();
            StaffName = new HashSet<EyeHealthStaffName>();
            OfficerToAct = new HashSet<EyeHealthOfficerToAct>();
        }
       public int EyeHealthId { get; set; }
       public string Reference { get; set; }
       public int ClientId { get; set; }
       public DateTime Date { get; set; }
       public DateTime Time { get; set; }
       public int ToolUsed { get; set; }
       public string ToolUsedAttach { get; set; }
       public int MethodUsed { get; set; }
       public string MethodUsedAttach { get; set; }
       public int TargetSet { get; set; }
       public int CurrentScore { get; set; }
       public int PatientGlasses { get; set; }
       public string Comment { get; set; }
       public int StatusImage { get; set; }
       public string StatusAttach { get; set; }
       public string PhysicianResponse { get; set; }
       public DateTime Deadline { get; set; }
       public string Remarks { get; set; }
       public int Status { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<EyeHealthOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<EyeHealthStaffName> StaffName { get; set; }
        public virtual ICollection<EyeHealthPhysician> Physician { get; set; }
    }

}
