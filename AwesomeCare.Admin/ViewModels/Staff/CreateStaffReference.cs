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
            OfficerToActList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }
        public string ActiveTab { get; set; } = "reference";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int StaffReferenceId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ApplicantRole { get; set; }
        [Required]
        public int DateofEmployement { get; set; }
        [Required]       
        public string DateofExit { get; set; }
        [Required]     
        public string RehireStaff { get; set; }
        [Required]
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
        public string RefreeName { get; set; }
        [Required]       
        public string Address { get; set; }
        [Required]       
        public string Email { get; set; }
        [Required]        
        public string Contact { get; set; }
        public string Attachment { get; set; }
        [Required]
        public int ConfirmedBy { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
