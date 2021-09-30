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
    public class CreateStaffSupervisionAppraisal
    {
        public CreateStaffSupervisionAppraisal()
        {
            OfficerToActList = new List<SelectListItem>();
            WorkteamList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> WorkteamList { get; set; }
        public string ActiveTab { get; set; } = "supervision";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int StaffSupervisionAppraisalId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public List<int> WorkTeam { get; set; }
        public List<string> WorkTeamName { get; set; }
        [Required]
        public int StaffRating { get; set; }
        [Required]
        public int ProfessionalDevelopment { get; set; }
        [Required]
        public int StaffComplaints { get; set; }
        [Required]
        public string FiveStarRating { get; set; }
        [Required]
        public string StaffDevelopment { get; set; }
        [Required]
        public string StaffAbility { get; set; }
        [Required]
        public string NoAbilityToSupport { get; set; }
        [Required]
        public string CondourAndWhistleBlowing { get; set; }
        [Required]
        public string NoCondourAndWhistleBlowing { get; set; }
        [Required]
        public int StaffSupportAreas { get; set; }
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
