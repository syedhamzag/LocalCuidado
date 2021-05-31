using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
  public  class PutStaffReferee
    {
        [Required]
        public int StaffRefereeId { get; set; }
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public string Referee { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PositionofReferee { get; set; }
        public string Attachment { get; set; }
        [Display(Name = "Change Certificate")]
        [JsonIgnore]
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile UploadAttachment { get; set; }
    }
}
