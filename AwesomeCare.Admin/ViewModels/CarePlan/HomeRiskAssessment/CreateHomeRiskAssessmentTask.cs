using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.HomeRiskAssessment
{
    public class CreateHomeRiskAssessmentTask
    {
        public int HomeRiskAssessmentTaskId { get; set; }
        public int HomeRiskAssessmentId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public bool IsSelected { get; set; }
    }
}
