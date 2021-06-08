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
            OfficerToActList = new List<GetStaffs>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        public ICollection<GetStaffs> OfficerToActList { get; set; }
        public string ActiveTab { get; set; } = "supervision";
        [Required]
        public int StaffSupervisionAppraisalId { get; set; }
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
        public int StaffRating { get; set; }
        [Required]
        public int ProfessionalDevelopment { get; set; }
        [Required]
        public int StaffComplaints { get; set; }
        [Required]
        [MaxLength(255)]
        public string FiveStarRating { get; set; }
        [Required]
        [MaxLength(255)]
        public string StaffDevelopment { get; set; }
        [Required]
        [MaxLength(255)]
        public string StaffAbility { get; set; }
        [Required]
        [MaxLength(255)]
        public string NoAbilityToSupport { get; set; }
        [Required]
        [MaxLength(255)]
        public string CondourAndWhistleBlowing { get; set; }
        [Required]
        [MaxLength(255)]
        public string NoCondourAndWhistleBlowing { get; set; }
        [Required]
        public int StaffSupportAreas { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRequired { get; set; }
        [Required]
        public int OfficerToAct { get; set; }
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
