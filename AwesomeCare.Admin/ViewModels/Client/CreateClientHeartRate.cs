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
    public class CreateClientHeartRate
    {
        public CreateClientHeartRate() 
        {
            OfficerToActList = new List<SelectListItem>();
            PhysicianList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        
        public IFormFile TargetHRAttachment { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile GenderAttachment { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile SeeChartAttachment { get; set; }
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> PhysicianList { get; set; }
        public ICollection<SelectListItem> StaffNameList { get; set; }
        public ICollection<SelectListItem> ClientList { get; set; }

        public string ActiveTab { get; set; } = "heartrate";
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string DOB { get; set; }
        public string IdNumber { get; set; }
        [Required]
        public int HeartRateId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int TargetHR { get; set; }
        public string TargetHRAttach { get; set; }
        [Required]
        public int Gender { get; set; }
        public string GenderAttach { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int BeatsPerSeconds { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        [Required]
        public List<int> StaffName { get; set; }
        public List<string> Staff_Name { get; set; }
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
        public string TargetHRName { get; set; }
        public string GenderName { get; set; }
        public string AgeName { get; set; }
        public string BeatsPerSecondsName { get; set; }
        public string SeeChartName { get; set; }
        public string OfficerToActName { get; set; }

    }
}
