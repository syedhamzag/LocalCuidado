using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment
{
    public class PostHomeRiskAssessment : BaseDTO
    {
        public PostHomeRiskAssessment()
        {
            PostHomeRiskAssessmentTask = new List<PostHomeRiskAssessmentTask>();
        }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string Heading { get; set; }


        public List<PostHomeRiskAssessmentTask> PostHomeRiskAssessmentTask { get; set; }
    }
}
