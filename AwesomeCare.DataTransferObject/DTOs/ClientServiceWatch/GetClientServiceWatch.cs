using AwesomeCare.DataTransferObject.DTOs.ClientService;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch
{
    public class GetClientServiceWatch
    {
        public GetClientServiceWatch()
        {
            OfficerToAct = new List<GetServiceOfficerToAct>();
            StaffName = new List<GetServiceStaffName>();
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

        public List<GetServiceOfficerToAct> OfficerToAct { get; set; }
        public List<GetServiceStaffName> StaffName { get; set; }
    }
}
