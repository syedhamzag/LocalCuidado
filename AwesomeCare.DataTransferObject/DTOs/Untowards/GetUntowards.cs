using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Untowards
{
   public class GetUntowards
    {
        public int UntowardsId { get; set; }
        public string TicketNumber { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string TimeOfCall { get; set; }
        public string PersonReporting { get; set; }
        public string PersonReportingTelephone { get; set; }
        public string PersonReportingEmail { get; set; }
        public string Details { get; set; }
        public string ActionStatus { get; set; }
        public string Priority { get; set; }
    }
}
