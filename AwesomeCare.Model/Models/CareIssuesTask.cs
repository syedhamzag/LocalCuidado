using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class CareIssuesTask
    {
        public int CareIssuesTaskId { get; set; }
        public int BestId { get; set; }

        public int Issues { get; set; }

        public virtual BestInterestAssessment BestInterestAssessment { get; set; }
    }

}
