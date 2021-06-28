using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientLogAudit
{
    public class CreateClientLogAudit
    {
        public CreateClientLogAudit()
        {
            OFFICERTOACT = new List<SelectListItem>();       
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]

        public IFormFile Evidence { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        #region DropDowns
        public ICollection<SelectListItem> OFFICERTOACT { get; set; }
        public ICollection<SelectListItem> Status_ { get; set; }
        #endregion

        public string ActiveTab { get; set; } = "logaudit";
        public int LogAuditId { get; set; }
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextDueDate { get; set; }
        [Required]
        public string IsCareExpected { get; set; }
        [Required]
        public string IsCareDifference { get; set; }
        [Required]
        public string ProperDocumentation { get; set; }
        [Required]
        public string ImproperDocumentation { get; set; }
        [Required]
        public string Communication { get; set; }
        [Required]
        public string ThinkingServiceUsers { get; set; }
        [Required]
        public string ThinkingStaff { get; set; }
        [Required]       
        public string ThinkingStaffStop { get; set; }
        [Required]       
        public string Observations { get; set; }
        [Required]        
        public string NameOfAuditor { get; set; }
        [Required]       
        public string ActionRecommended { get; set; }
        [Required]        
        public string ActionTaken { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
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
        public string EvidenceFilePath { get; set; }
    }
}
