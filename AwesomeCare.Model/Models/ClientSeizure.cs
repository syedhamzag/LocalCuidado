using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientSeizure
    { 

        public ClientSeizure()
        {
            Physician = new HashSet<SeizurePhysician>();
            StaffName = new HashSet<SeizureStaffName>();
            OfficerToAct = new HashSet<SeizureOfficerToAct>();
        }
        public int SeizureId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int SeizureType { get; set; }
        public string SeizureTypeAttach { get; set; }
        public int SeizureLength { get; set; }
        public string SeizureLengthAttach { get; set; }
        public int Often { get; set; }
        public string WhatHappened { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }   
    
        public virtual Client Client { get; set; }

        public virtual ICollection<SeizureOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<SeizureStaffName> StaffName { get; set; }
        public virtual ICollection<SeizurePhysician> Physician { get; set; }

    }

}
