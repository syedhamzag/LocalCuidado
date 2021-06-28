using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker
{
    public class PostStaffKeyWorkerVoice
    {
        public PostStaffKeyWorkerVoice()
        {
            OfficerToAct = new List<PostKeyWorkerOfficerToAct>();
            Workteam = new List<PostKeyWorkerWorkteam>();
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

        public List<PostKeyWorkerOfficerToAct> OfficerToAct { get; set; }
        public List<PostKeyWorkerWorkteam> Workteam { get; set; }
    }
}
