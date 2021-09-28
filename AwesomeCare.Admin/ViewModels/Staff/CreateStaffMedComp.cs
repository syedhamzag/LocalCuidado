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
    public class CreateStaffMedComp
    {
        public CreateStaffMedComp()
        {
            OfficerToActList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }
        public string ActiveTab { get; set; } = "medcomp";
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        public string Reference { get; set; }
        [Required]
        public int MedCompId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int UnderstandingofMedication { get; set; }
        [Required]
        public int UnderstandingofRights { get; set; }
        [Required]
        public int ReadingMedicalPrescriptions { get; set; }
        [Required]
        public int CarePlan { get; set; }
        [Required]
        public int RateStaff { get; set; }
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
