using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClientBloodCoagulationRecord
    {
        public CreateClientBloodCoagulationRecord() 
        {
            OfficerToActList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            PhysicianList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile TargetINRAttachment { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StaffNameList { get; set; }
        public ICollection<SelectListItem> PhysicianList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }

        public string ActiveTab { get; set; } = "bloodcoagrecord";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int BloodRecordId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Indication { get; set; }
        [Required]
        public int TargetINR { get; set; }
        public string TargetINRAttach { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int CurrentDose { get; set; }
        [Required]
        public int INR { get; set; }
        [Required]
        public int NewDose { get; set; }
        [Required]
        public int NewINR { get; set; }
        [Required]
        public int BloodStatus { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public List<int> StaffName { get; set; }
        public List<string> Staff_Name { get; set; }
        [Required]
        public List<int> Physician { get; set; }
        public List<string> PhysicianName { get; set; }
        [Required]
        public string PhysicianResponce { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        public List<string> OfficerToActName { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
