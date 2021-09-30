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
            OfficerToActList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public string ActiveTab { get; set; } = "onetoone";
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        public string Reference { get; set; }
        [Required]
        public int OneToOneId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]        
        public string Purpose { get; set; }
        [Required]
        public int PreviousSupervision { get; set; }
        [Required]        
        public string StaffImprovedInAreas { get; set; }
        [Required]       
        public string CurrentEventArea { get; set; }
        [Required]        
        public string StaffConclusion { get; set; }
        [Required]       
        public string DecisionsReached { get; set; }
        [Required]       
        public string ImprovementRecorded { get; set; }
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
