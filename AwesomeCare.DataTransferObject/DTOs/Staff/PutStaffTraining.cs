using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
  public  class PutStaffTraining
    {
        [Required]
        public int StaffTrainingId { get; set; }
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public string Training { get; set; }
        [Required]
        public string Certificate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Trainer { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string ExpiredDate { get; set; }
        public string CertificateAttachment { get; set; }
        [Display(Name = "Change Certificate")]
        [JsonIgnore]
        public IFormFile UploadAttachment { get; set; }
    }
}
