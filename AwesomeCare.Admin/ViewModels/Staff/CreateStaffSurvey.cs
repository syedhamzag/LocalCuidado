using AwesomeCare.DataTransferObject.DTOs.Staff;
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
    public class CreateStaffSurvey
    {
        public CreateStaffSurvey()
        {
            OfficerToActList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public string ActiveTab { get; set; } = "survey";
        public List<int> SurveyIds { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int StaffSurveyId { get; set; }
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
        public int WorkTeam { get; set; }
        [Required]
        public int AdequateTrainingReceived { get; set; }
        [Required]
        public int HealthCareServicesSatisfaction { get; set; }
        [Required]
        public int SupportFromCompany { get; set; }
        [Required]
        public int CompanyManagement { get; set; }
        [Required]
        public int AccessToPolicies { get; set; }
        [Required]
        [MaxLength(255)]
        public string WorkEnvironmentSuggestions { get; set; }
        [Required]
        [MaxLength(255)]
        public string AreaRequiringImprovements { get; set; }
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
