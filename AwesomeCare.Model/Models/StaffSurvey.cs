using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffSurvey
    {
        public StaffSurvey()
        {
            OfficerToAct = new HashSet<SurveyOfficerToAct>();
            Workteam = new HashSet<SurveyWorkteam>();
        }
        public int StaffSurveyId { get; set; }
        public string Reference { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public string Details { get; set; }
        public int AdequateTrainingReceived { get; set; }
        public int HealthCareServicesSatisfaction { get; set; }
        public int SupportFromCompany { get; set; }
        public int CompanyManagement { get; set; }
        public int AccessToPolicies { get; set; }
        public string WorkEnvironmentSuggestions { get; set; }
        public string AreaRequiringImprovements { get; set; }
        public string ActionRequired { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public virtual ICollection<SurveyOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<SurveyWorkteam> Workteam { get; set; }
    }
}
