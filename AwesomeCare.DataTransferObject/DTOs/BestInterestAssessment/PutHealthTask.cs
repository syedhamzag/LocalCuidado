using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PutHealthTask
    {
        public int HealthTaskId { get; set; }
        public int BestId { get; set; }
        public int HeadingId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Remarks { get; set; }
    }
}
