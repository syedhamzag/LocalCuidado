using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientBloodCoagulationRecord
    {
        public ClientBloodCoagulationRecord()
        {
            Physician = new HashSet<BloodCoagPhysician>();
            StaffName = new HashSet<BloodCoagStaffName>();
            OfficerToAct = new HashSet<BloodCoagOfficerToAct>();
        }
        public string Reference { get; set; }
       public int BloodRecordId { get; set; }
       public int ClientId { get; set; }
       public DateTime Date { get; set; }
       public DateTime Time { get; set; }
       public int Indication { get; set; }
       public int TargetINR { get; set; }
       public string TargetINRAttach { get; set; }
       public DateTime StartDate { get; set; }
       public int CurrentDose { get; set; }
       public int INR { get; set; }
       public int NewDose { get; set; }
       public int NewINR { get; set; }
       public int BloodStatus { get; set; }
       public string Comment { get; set; }
       public string PhysicianResponce { get; set; }
       public DateTime Deadline { get; set; }
       public string Remark { get; set; }
       public int Status { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<BloodCoagOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<BloodCoagStaffName> StaffName { get; set; }
        public virtual ICollection<BloodCoagPhysician> Physician { get; set; }
    }

}
