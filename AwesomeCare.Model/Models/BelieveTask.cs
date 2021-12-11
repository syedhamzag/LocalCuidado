using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class BelieveTask
    {
        public int BelieveTaskId { get; set; }
        public int BestId { get; set; }
       
        public string ReasonableBelieve { get; set; }
        public virtual BestInterestAssessment BestInterestAssessment { get; set; }
    }
}
