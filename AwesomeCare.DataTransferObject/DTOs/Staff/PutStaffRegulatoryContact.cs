using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
  public  class PutStaffRegulatoryContact
    {
        [Required]
        public int StaffRegulatoryContactId { get; set; }
        [Required]
        public int StaffPersonalInfoId { get; set; }
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }
        [Display(Name = "Change Certificate")]
        [JsonIgnore]
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg",".pdf" })]
        public IFormFile UploadAttachment { get; set; }
    }
}
