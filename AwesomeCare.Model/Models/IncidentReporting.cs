using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class IncidentReporting
    {
        public int IncidentReportingId { get; set; }
        public int? ReportingStaffId { get; set; }
        public int ClientId { get; set; }
        public int StaffInvolvedId { get; set; }
        public int IncidentTypeId { get; set; }
        public string IncidentDetails { get; set; }
        public string ActionTaken { get; set; }
        public string Witness { get; set; }
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
    }
}
