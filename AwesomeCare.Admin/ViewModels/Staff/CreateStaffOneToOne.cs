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
    public class CreateStaffOneToOne
    {
        public CreateStaffOneToOne()
        {
            OfficerToActList = new List<GetStaffs>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        public ICollection<GetStaffs> OfficerToActList { get; set; }
        public string ActiveTab { get; set; } = "onetoone";
        [Required]
        public int OneToOneId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        [MaxLength(255)]
        public string Purpose { get; set; }
        [Required]
        public int PreviousSupervision { get; set; }
        [Required]
        [MaxLength(255)]
        public string StaffImprovedInAreas { get; set; }
        [Required]
        [MaxLength(255)]
        public string CurrentEventArea { get; set; }
        [Required]
        [MaxLength(255)]
        public string StaffConclusion { get; set; }
        [Required]
        [MaxLength(255)]
        public string DecisionsReached { get; set; }
        [Required]
        [MaxLength(255)]
        public string ImprovementRecorded { get; set; }
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
