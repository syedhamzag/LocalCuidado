using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.FileUpload
{
  public  class PostFile
    {
        [Required(ErrorMessage ="Please upload a file")]
        [AllowedExtensions(new string[] { ".pdf",".jpeg",".jpg",".png"})]
        public IFormFile File { get; set; }

        /// <summary>
        /// container to save the file
        /// </summary>
        public string FolderName { get; set; }

        [Required]
        [MaxLength(15)]
        public string FileName { get; set; }
    }
}
