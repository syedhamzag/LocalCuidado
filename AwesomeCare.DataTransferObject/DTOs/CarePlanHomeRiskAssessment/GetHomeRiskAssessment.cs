using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment
{
    public class GetHomeRiskAssessment : BaseDTO
    {
        public GetHomeRiskAssessment()
        {
            GetHomeRiskAssessmentTask = new List<GetHomeRiskAssessmentTask>();
        }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string Heading { get; set; }


        public List<GetHomeRiskAssessmentTask> GetHomeRiskAssessmentTask { get; set; }
    }
}
