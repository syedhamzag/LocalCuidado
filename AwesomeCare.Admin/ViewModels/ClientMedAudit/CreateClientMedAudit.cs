﻿using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientMedAudit
{
    public class CreateClientMedAudit
    {
        public CreateClientMedAudit()
        {
            OFFICERTOACTList = new List<SelectListItem>();
            AuditorList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        
        public IFormFile Evidence { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }
        #region DropDowns
        public ICollection<SelectListItem> OFFICERTOACTList { get; set; }

        public ICollection<SelectListItem> AuditorList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        #endregion
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string DOB { get; set; }
        public string IdNumber { get; set; }
        public string ActiveTab { get; set; } = "medaudit";
        public int MedAuditId { get; set; }
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextDueDate { get; set; }
        [Required]
        public int MedicationConcern { get; set; }
        [Required]
        public int MarChartReview { get; set; }
        [Required]
        public int HardCopyReview { get; set; }
        [Required]
        public string RightsOfMedication { get; set; }
        [Required]
        public int GapsInAdmistration { get; set; }
        [Required]
        public string ThinkingServiceUsers { get; set; }
        [Required]
        public int MedicationSupplyEfficiency { get; set; }
        [Required]
        public int MedicationInfoUploadEefficiency { get; set; }
        [Required]
        public string Observations { get; set; }
        [Required]
        public List<int> NameOfAuditor { get; set; }
        public List<string> Auditor_Name { get; set; }
        [Required]
        public string ActionRecommended { get; set; }
        [Required]
        public string ActionTaken { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        public List<int> OfficerToTakeAction { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        [Display(Name = "Status_")]
        public int Status { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public int RepeatOfIncident { get; set; }
        [Required]
        public string RotCause { get; set; }
        [Required]
        public string LessonLearntAndShared { get; set; }
        [Required]
        public string LogURL { get; set; }
        public string Attachment { get; set; }
        public string MedicationConcernName { get; set; }
        public string MarChartReviewName { get; set; }
        public string HardCopyReviewName { get; set; }
        public string GapsInAdmistrationName { get; set; }
        public string MedicationSupplyEfficiencyName { get; set; }
        public string MedicationInfoUploadEefficiencyName { get; set; }
        public string RepeatOfIncidentName { get; set; }


    }
}
