using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment
{
    public class PutHomeRiskAssessment:BaseDTO
    {
        public PutHomeRiskAssessment()
        {
            PutHomeRiskAssessmentTask = new List<PutHomeRiskAssessmentTask>();
        }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string Heading { get; set; }


        public List<PutHomeRiskAssessmentTask> PutHomeRiskAssessmentTask { get; set; }
    }
}
