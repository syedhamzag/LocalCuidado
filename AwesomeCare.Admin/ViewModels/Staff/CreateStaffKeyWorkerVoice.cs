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
            ClientList = new List<SelectListItem>();
            WorkList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> WorkList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }
        public string ActiveTab { get; set; } = "keyworker";
        public string Reference { get; set; }
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        
        [Required]
        public int KeyWorkerId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public List<int> TeamYouWorkFor { get; set; }
        public List<string> WorkteamName { get; set; }
        [Required]
        public int NotComfortableServices { get; set; }
        [Required]
        public int ServicesRequiresTime { get; set; }
        [Required]
        public int ServicesRequiresServices { get; set; }
        [Required]
        public int WellSupportedServices { get; set; }
        [Required]
        public string ChangesWeNeed { get; set; }
        [Required]
        public string NutritionalChanges { get; set; }
        [Required]
        public string HealthAndWellNessChanges { get; set; }
        [Required]
        public string MedicationChanges { get; set; }
        [Required]       
        public string MovingAndHandling { get; set; }
        [Required]        
        public string RiskAssessment { get; set; }
        [Required]
        public string ActionRequired { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
