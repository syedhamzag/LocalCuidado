using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientDailyTask
{
    public class PostClientDailyTask
    {
        public int DailyTaskId { get; set; }
        public int ClientId { get; set; }
        public string DailyTaskName { get; set; }
        public DateTime Date { get; set; }
        public DateTime AmendmentDate { get; set; }
        public string Attachment { get; set; }
    }
}
