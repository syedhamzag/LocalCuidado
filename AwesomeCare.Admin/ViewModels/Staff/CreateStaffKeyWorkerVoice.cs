using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffKeyWorkerVoice
    {
        public CreateStaffKeyWorkerVoice()
        {
            OfficerToActList = new List<SelectListItem>();
            ClientList = new List<GetClient>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<GetClient> ClientList { get; set; }
        public string ActiveTab { get; set; } = "keyworker";
        public List<int> KeyWorkerIds { get; set; }
        public string Reference { get; set; }

        [Required]
        public int KeyWorkerId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        [MaxLength(255)]
        public string Details { get; set; }
        [Required]
        public int TeamYouWorkFor { get; set; }
        [Required]
        public int NotComfortableServices { get; set; }
        [Required]
        public int ServicesRequiresTime { get; set; }
        [Required]
        public int ServicesRequiresServices { get; set; }
        [Required]
        public int WellSupportedServices { get; set; }
        [Required]
        [MaxLength(255)]
        public string ChangesWeNeed { get; set; }
        [Required]
        [MaxLength(255)]
        public string NutritionalChanges { get; set; }
        [Required]
        [MaxLength(255)]
        public string HealthAndWellNessChanges { get; set; }
        [Required]
        [MaxLength(255)]
        public string MedicationChanges { get; set; }
        [Required]
        [MaxLength(255)]
        public string MovingAndHandling { get; set; }
        [Required]
        [MaxLength(255)]
        public string RiskAssessment { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRequired { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        [MaxLength(255)]
        public string Remarks { get; set; }
        [Required]
        [MaxLength(255)]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
