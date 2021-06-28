using AwesomeCare.DataTransferObject.DTOs.ClientService;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch
{
    public class PutClientServiceWatch
    {
        public PutClientServiceWatch()
        {
            OfficerToAct = new List<PutServiceOfficerToAct>();
            StaffName = new List<PutServiceStaffName>();
        }

        public int WatchId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public int Incident { get; set; }
        public int Details { get; set; }
        public int Contact { get; set; }
        public string Observation { get; set; }
        public string ActionRequired { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public List<PutServiceOfficerToAct> OfficerToAct { get; set; }
        public List<PutServiceStaffName> StaffName { get; set; }
    }
}
