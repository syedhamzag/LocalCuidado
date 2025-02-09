﻿using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientMgtVisit
{
    public class CreateClientMgtVisit
    {
        public CreateClientMgtVisit()
        {
            OfficerToActList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
            StaffList = new List<SelectListItem>();

        }
        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile Evidence { get; set; }
        #region DropDowns
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        public ICollection<SelectListItem> StaffList { get; set; }
        #endregion
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string DOB { get; set; }
        public string IdNumber { get; set; }
        public string ActiveTab { get; set; } = "mgtvisit";
        public int VisitId { get; set; }
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public int RateServiceRecieving { get; set; }
        [Required]
        public int RateManagers { get; set; }
        [Required]
        public List<int> StaffBestSupport { get; set; }
        public List<string> Staff_Name { get; set; }
        [Required]
        public int HowToComplain { get; set; }
        [Required]
        public int ServiceRecommended { get; set; }
        [Required]
        public string ImprovementExpect { get; set; }
        [Required]
        public string Observation { get; set; }
        [Required]
        public string ActionRequired { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public List<int> OfficerToAct { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        public string ActionsTakenByMPCC { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string RotCause { get; set; }
        [Required]
        public string LessonLearntAndShared { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string URL { get; set; }
        public string Attachment { get; set; }
        public string RateServiceRecievingName { get; set; }
        public string RateManagersName { get; set; }
        public string HowToComplainName { get; set; }
        public string ServiceRecommendedName { get; set; }
        public string OfficerToActName { get; set; }
    }
}
