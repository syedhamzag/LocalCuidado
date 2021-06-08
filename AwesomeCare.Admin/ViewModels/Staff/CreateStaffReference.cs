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
    public class CreateStaffReference
    {
        public CreateStaffReference()
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
        public string ActiveTab { get; set; } = "reference";
        [Required]
        public int StaffReferenceId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ApplicantRole { get; set; }
        [Required]
        public int DateofEmployement { get; set; }
        [Required]
        [MaxLength(255)]
        public string DateofExit { get; set; }
        [Required]
        [MaxLength(255)]
        public string RehireStaff { get; set; }
        [Required]
        [MaxLength(255)]
        public string Relationship { get; set; }
        [Required]
        public int TeamWork { get; set; }
        [Required]
        public int Integrity { get; set; }
        [Required]
        public int Knowledgeable { get; set; }
        [Required]
        public int WorkUnderPressure { get; set; }
        [Required]
        public int Caring { get; set; }
        [Required]
        public int PreviousExperience { get; set; }
        [Required]
        [MaxLength(255)]
        public string RefreeName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Contact { get; set; }
        public string Attachment { get; set; }
        [Required]
        public int ConfirmedBy { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
