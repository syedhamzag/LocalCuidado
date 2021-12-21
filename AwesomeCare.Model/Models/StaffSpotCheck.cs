using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffSpotCheck
    {
        public StaffSpotCheck()
        {
            OfficerToAct = new HashSet<SpotCheckOfficerToAct>();
        }
        public int SpotCheckId {get; set;}
        public string Reference { get; set; }
        public int StaffId {get; set;}
        public DateTime Date {get; set;}
        public DateTime NextCheckDate {get; set;}
        public string Details { get; set; }
        public int ClientId {get; set;}
        public int StaffArriveOnTime {get; set;}
        public int StaffDressCode {get; set;}
        public string AreaComments{get; set;}
        public string ActionRequired{get; set;}
        public DateTime Deadline {get; set;}
        public int Status {get; set;}
        public string Remarks{get; set;}
        public string URL{get; set;}
        public string Attachment {get; set;}

        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo Staff { get; set; }
        public virtual ICollection<SpotCheckOfficerToAct> OfficerToAct { get; set; }
    }
}
