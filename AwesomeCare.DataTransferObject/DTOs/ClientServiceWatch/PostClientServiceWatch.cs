using AwesomeCare.DataTransferObject.DTOs.ClientService;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch
{
    public class PostClientServiceWatch
    {
        public PostClientServiceWatch()
        {
            OfficerToAct = new List<PostServiceOfficerToAct>();
            StaffName = new List<PostServiceStaffName>();
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

        public List<PostServiceOfficerToAct> OfficerToAct { get; set; }
        public List<PostServiceStaffName> StaffName { get; set; }
    }
}
