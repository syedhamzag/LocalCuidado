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
            WorkteamList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        public IFormFile Attach { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> WorkteamList { get; set; }
        public string ActiveTab { get; set; } = "survey";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
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
        public string Details { get; set; }
        [Required]
        public List<int> WorkTeam { get; set; }
        public List<string> WorkteamName { get; set; }
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
        public string WorkEnvironmentSuggestions { get; set; }
        [Required]
        public string AreaRequiringImprovements { get; set; }
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
        public string StaffName { get; set; }
    }
}
