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
            OFFICERTOACT = new List<GetStaffs>();       
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Evidence { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }
        #region DropDowns
        public ICollection<GetStaffs> OFFICERTOACT { get; set; }
        public ICollection<SelectListItem> Status_ { get; set; }
        #endregion
        public string ActiveTab { get; set; } = "medaudit";
        public int MedAuditId { get; set; }
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
        [MaxLength(255)]
        public string RightsOfMedication { get; set; }
        [Required]
        public int GapsInAdmistration { get; set; }
        [Required]
        [MaxLength(255)]
        public string ThinkingServiceUsers { get; set; }
        [Required]
        public int MedicationSupplyEfficiency { get; set; }
        [Required]
        public int MedicationInfoUploadEefficiency { get; set; }
        [Required]
        [MaxLength(255)]
        public string Observations { get; set; }
        [Required]
        [MaxLength(255)]
        public string NameOfAuditor { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRecommended { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionTaken { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public int OfficerToTakeAction { get; set; }
        [Required]
        [Display(Name = "Status_")]
        public int Status { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        [MaxLength(255)]
        public string Remarks { get; set; }
        [Required]
        public int RepeatOfIncident { get; set; }
        [Required]
        [MaxLength(50)]
        public string RotCause { get; set; }
        [Required]
        [MaxLength(255)]
        public string LessonLearntAndShared { get; set; }
        [Required]
        [MaxLength(255)]
        public string LogURL { get; set; }
        public string Attachment { get; set; }
    }
}
