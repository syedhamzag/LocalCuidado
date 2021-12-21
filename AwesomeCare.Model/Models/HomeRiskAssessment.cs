using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HomeRiskAssessment : Base.BaseModel
    {
        public HomeRiskAssessment()
        {
            HomeRiskAssessmentTask = new HashSet<HomeRiskAssessmentTask>();
        }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string Heading { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<HomeRiskAssessmentTask> HomeRiskAssessmentTask { get; set; }
    }
}
