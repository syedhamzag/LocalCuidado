using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.DutyOnCall
{
    public class OnCall
    {
        public int DutyOnCallId { get; set; }
        public DateTime Date { get; set; }
        public string Concern { get; set; }
        public string StatusName { get; set; }
        public string ClientName { get; set; }
        public string StaffName { get; set; }
        public string NotificationStatusName { get; set; }
    }
}
