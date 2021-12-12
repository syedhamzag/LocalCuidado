using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PostHealthTask
    {
        public int HealthTaskId { get; set; }
        public int BestId { get; set; }
        public int HeadingId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
