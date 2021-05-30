﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientMedAudit
    {
        public int MedAuditId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextDueDate { get; set; }
        public int GapsInAdmistration { get; set; }
        public string RightsOfMedication { get; set; }
        public int MarChartReview { get; set; }
        public int MedicationConcern { get; set; }
        public int HardCopyReview { get; set; }
        public string ThinkingServiceUsers { get; set; }
        public int MedicationSupplyEfficiency { get; set; }
        public int MedicationInfoUploadEefficiency { get; set; }
        public string Observations { get; set; }
        public string NameOfAuditor { get; set; }
        public string ActionRecommended { get; set; }
        public string ActionTaken { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        public int OfficerToTakeAction { get; set; }
        public int Status { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public bool RepeatOfIncident { get; set; }
        public string RotCause { get; set; }
        public string LessonLearntAndShared { get; set; }
        public string LogURL { get; set; }
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo Staff { get; set; }

    }
}
