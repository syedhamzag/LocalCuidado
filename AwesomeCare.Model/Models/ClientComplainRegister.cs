using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientComplainRegister
    {
        public ClientComplainRegister()
        {
            StaffName = new HashSet<ComplainStaffName>();
            OfficerToAct = new HashSet<ComplainOfficerToAct>();
        }
            public int ComplainId { get; set; }
            public string Reference { get; set; }
            public int ClientId { get; set; }
            public string LINK { get; set; }
            public string IRFNUMBER { get; set; }
            public DateTime INCIDENTDATE { get; set; }
            public DateTime DATERECIEVED { get; set; }
            public DateTime DATEOFACKNOWLEDGEMENT { get; set; }
            public string SOURCEOFCOMPLAINTS { get; set; }
            public string COMPLAINANTCONTACT { get; set; }
            public string CONCERNSRAISED { get; set; }
            public DateTime DUEDATE { get; set; }
            public string LETTERTOSTAFF { get; set; }
            public string INVESTIGATIONOUTCOME { get; set; }
            public string ACTIONTAKEN { get; set; }
            public string FINALRESPONSETOFAMILY { get; set; }
            public string ROOTCAUSE { get; set; }
            public string REMARK { get; set; }
            public int StatusId { get; set; }
            public string EvidenceFilePath { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<ComplainOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<ComplainStaffName> StaffName { get; set; }
    }
}
