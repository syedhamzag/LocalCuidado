using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition
{
    public class PostSpecialHealthCondition
    {
        public int HealthCondId { get; set; }
        public int ClientId { get; set; }
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
