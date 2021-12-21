using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment
{
    public class PostHomeRiskAssessmentTask : BaseDTO
    {
        public int HomeRiskAssessmentTaskId { get; set; }
        public int HomeRiskAssessmentId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
    }
}
