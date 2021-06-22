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
    public class CreateClientWoundCare
    {
        public CreateClientWoundCare() 
        {
            OfficerToActList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            PhysicianList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile StatusAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile UlcerStageAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile TypeAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile MeasurementAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile LocationAttachment { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StaffNameList { get; set; }
        public ICollection<SelectListItem> PhysicianList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }

        public string ActiveTab { get; set; } = "woundcare";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int WoundCareId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Goal { get; set; }
        [Required]
        public int Type { get; set; }
        public string TypeAttach { get; set; }
        [Required]
        public int UlcerStage { get; set; }
        public string UlcerStageAttach { get; set; }
        [Required]
        public int Measurment { get; set; }
        public string MeasurementAttach { get; set; }
        [Required]
        public int PainLvl { get; set; }
        [Required]
        public int Location { get; set; }
        public string LocationAttach { get; set; }
        [Required]
        public int WoundCause { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        [Required]
        public List<int> StaffName { get; set; }
        [Required]
        public List<int> Physician { get; set; }
        [Required]
        public string PhysicianResponse { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
