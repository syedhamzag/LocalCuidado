using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.Health
{
    public class CreateSpecialHealthCondition
    {
        public int HealthCondId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ConditionName { get; set; }
        public string SourceInformation { get; set; }
        public string FeelingBeforeIncident { get; set; }
        public string FeelingAfterIncident { get; set; }
        public string Frequency { get; set; }
        public string LivingActivities { get; set; }
        public string Trigger { get; set; }
        public string ClientAction { get; set; }
        public string ClinicRecommendation { get; set; }
        public string LifestyleSupport { get; set; }
        public string PlanningHealthCondition { get; set; }
    }
}
