using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
  public  class GetIncidentReport
    {
        public int IncidentReportingId { get; set; }
        public int? ReportingStaffId { get; set; }
        public string ReportingStaff { get; set; }
        public int ClientId { get; set; }
        public string Client { get; set; }
        public int StaffInvolvedId { get; set; }
        public string StaffInvolved { get; set; }
        public int IncidentTypeId { get; set; }
        public string IncidentType { get; set; }
        public string IncidentDetails { get; set; }
        public string ActionTaken { get; set; }
        public string Witness { get; set; }
        public string Attachment { get; set; }
    }
}
