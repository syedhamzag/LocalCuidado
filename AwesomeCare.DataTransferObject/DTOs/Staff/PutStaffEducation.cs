using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
    public class PutStaffEducation
    {
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public int StaffEducationId { get; set; }
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
        [DataType(DataType.Date)]
        public string EndDate { get; set; }
        public string CertificateAttachment { get; set; }

        [Display(Name = "Change Certificate")]
        [JsonIgnore]
        public IFormFile UploadCertificate { get; set; }
    }
}
