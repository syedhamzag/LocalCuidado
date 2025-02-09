﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Company
{
  public  class UpdateCompanyDto
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string LogoUrl { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [Required]
        public string Language { get; set; }
    }
}
