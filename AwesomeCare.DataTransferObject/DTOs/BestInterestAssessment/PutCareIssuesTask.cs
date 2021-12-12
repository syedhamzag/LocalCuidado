using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PutCareIssuesTask
    {
        public int CareIssuesTaskId { get; set; }
        public int BestId { get; set; }

        public int Issues { get; set; }
    }
}
