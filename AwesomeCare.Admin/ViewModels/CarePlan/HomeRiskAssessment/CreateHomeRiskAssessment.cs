using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.HomeRiskAssessment
{
    public class CreateHomeRiskAssessment
    {
        public CreateHomeRiskAssessment()
        {
            Tasks = new List<CreateHomeRiskAssessmentTask>();
        }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Heading { get; set; }
        public List<CreateHomeRiskAssessmentTask> Tasks { get; set; }
    }
}
