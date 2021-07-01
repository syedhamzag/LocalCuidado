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
    public class CreateClientSeizure
    {
        public CreateClientSeizure() 
        {
            OfficerToActList = new List<SelectListItem>();
            PhysicianList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile StatusAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile SeizureTypeAttachment { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile SeizureLengthAttachment { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> PhysicianList { get; set; }
        public ICollection<SelectListItem> StaffNameList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }

        public string ActiveTab { get; set; } = "seizure";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int SeizureId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int SeizureType { get; set; }
        public string SeizureTypeAttach { get; set; }
        [Required]
        public int SeizureLength { get; set; }
        public string SeizureLengthAttach { get; set; }
        [Required]
        public int Often { get; set; }
        [Required]
        public string WhatHappened { get; set; }
        [Required]
        public List<int> StaffName { get; set; }
        public List<string> Staff_Name { get; set; }
        [Required]
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        [Required]
        public List<int> Physician { get; set; }
        public List<string> PhysicianName { get; set; }
        [Required]
        public string PhysicianResponse { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
