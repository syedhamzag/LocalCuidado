using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PostHealthTask2
    {
        public int HealthTask2Id { get; set; }
        public int BestId { get; set; }
        public int HeadingId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
