using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.IncidentReporting
{
    public class PostIncidentReport
    {
        public int? ReportingStaffId { get; set; }
        public int ClientId { get; set; }
        public int StaffInvolvedId { get; set; }
        public int IncidentTypeId { get; set; }
        public string IncidentDetails { get; set; }
        public string ActionTaken { get; set; }
        public string Witness { get; set; }
        public string Attachment { get; set; }
    }
}
