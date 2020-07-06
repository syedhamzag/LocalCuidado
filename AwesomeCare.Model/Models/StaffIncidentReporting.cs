using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffIncidentReporting
    {
        public int StaffIncidentReportingId { get; set; }
        public int? ReportingStaffId { get; set; }
        public int ClientId { get; set; }
        public int StaffInvolvedId { get; set; }
        /// <summary>
        /// From Base Record
        /// </summary>
        public int IncidentType { get; set; }
        public string IncidentDetails { get; set; }
        public string ActionTaken { get; set; }
        public string Witness { get; set; }
        public string Attachment { get; set; }
        /// <summary>
        /// ApplicationUserId
        /// </summary>
        public string LoggedById { get; set; }
        public DateTimeOffset LoggedDate { get; set; }
    }
}
