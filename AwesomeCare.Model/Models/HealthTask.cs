using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HealthTask
    {
        public int HealthTaskId { get; set; }
        public int BestId { get; set; }
        public int HeadingId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public virtual BestInterestAssessment BestInterestAssessment { get; set; }
    }
}
