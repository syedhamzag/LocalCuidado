﻿using AwesomeCare.DataTransferObject.DTOs.Client;
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
    public class CreateClientPainChart
    {
        public CreateClientPainChart() 
        {
            OfficerToActList = new List<SelectListItem>();
            PhysicianList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        
        public IFormFile StatusAttachment { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile LocationAttachment { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile TypeAttachment { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> PhysicianList { get; set; }
        public ICollection<SelectListItem> StaffNameList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }

        public string ActiveTab { get; set; } = "painchart";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string DOB { get; set; }
        public string IdNumber { get; set; }
        [Required]
        public int PainChartId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Type { get; set; }
        public string TypeAttach { get; set; }
        [Required]
        public int Location { get; set; }
        public string LocationAttach { get; set; }
        [Required]
        public int PainLvl { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        [Required]
        public List<int> StaffName { get; set; }
        public string _StaffName { get; set; }
        public List<string> Staff_Name { get; set; }
        [Required]
        public List<int> Physician { get; set; }
        public string _PhysicianName { get; set; }
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
        public string TypeName { get; set; }
        public string LocationName { get; set; }
        public string PainLvlName { get; set; }
        public string StatusImageName { get; set; }
        public string OfficerToActName { get; set; }

    }
}
