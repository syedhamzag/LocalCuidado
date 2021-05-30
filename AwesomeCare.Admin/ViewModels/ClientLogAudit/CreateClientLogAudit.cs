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
        public string ActiveTab { get; set; } = "logaudit";
        public int LogAuditId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextDueDate { get; set; }
        [Required]
        [MaxLength(255)]
        public string IsCareExpected { get; set; }
        [Required]
        [MaxLength(255)]
        public string IsCareDifference { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProperDocumentation { get; set; }
        [Required]
        [MaxLength(255)]
        public string ImproperDocumentation { get; set; }
        [Required]
        [MaxLength(255)]
        public string Communication { get; set; }
        [Required]
        [MaxLength(255)]
        public string ThinkingServiceUsers { get; set; }
        [Required]
        [MaxLength(255)]
        public string ThinkingStaff { get; set; }
        [Required]
        [MaxLength(255)]
        public string ThinkingStaffStop { get; set; }
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
        public bool RepeatOfIncident { get; set; }
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
