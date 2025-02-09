﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaffEducation
    {
        [Required]
        public string Institution { get; set; }
        [Required]
        public string Certificate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IFormFile CertificateAttachment { get; set; }
    }
}
