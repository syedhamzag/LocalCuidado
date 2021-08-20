using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffKeyWorkerVoice
    {
        public StaffKeyWorkerVoice()
        {
            OfficerToAct = new HashSet<KeyWorkerOfficerToAct>();
            Workteam = new HashSet<KeyWorkerWorkteam>();
        }
        public int KeyWorkerId { get; set; }
        public string Reference { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public string Details { get; set; }
        public int NotComfortableServices { get; set; }
        public int ServicesRequiresTime { get; set; }
        public int ServicesRequiresServices { get; set; }
        public int WellSupportedServices { get; set; }
        public string ChangesWeNeed { get; set; }
        public string NutritionalChanges { get; set; }
        public string HealthAndWellNessChanges { get; set; }
        public string MedicationChanges { get; set; }
        public string MovingAndHandling { get; set; }
        public string RiskAssessment { get; set; }
        public string ActionRequired { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo Staff { get; set; }
        public virtual ICollection<KeyWorkerOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<KeyWorkerWorkteam> Workteam { get; set; }
    }
}
