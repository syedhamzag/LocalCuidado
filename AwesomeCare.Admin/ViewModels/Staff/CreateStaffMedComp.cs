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
            OfficerToActList = new List<GetStaffs>();
            ClientList = new List<GetClient>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        public ICollection<GetStaffs> OfficerToActList { get; set; }
        public ICollection<GetClient> ClientList { get; set; }
        public string ActiveTab { get; set; } = "medcomp";
        [Required]
        public int MedCompId { get; set; }
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
