﻿using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Admin.ViewModels.IncomingMeds
{
    public class CreateIncomingMeds
    {
        public CreateIncomingMeds()
        {
            ClientList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        public IFormFile Attach { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        public ICollection<SelectListItem> ClientList { get; set; }
        public int IncomingMedsId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int UserName { get; set; }
        [Required]
        public string StaffName { get; set; }
        [Required]
        public string StartDate { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int Status { get; set; }
        public string ChartImage { get; set; }
        public string MedsImage { get; set; }
    }
}
