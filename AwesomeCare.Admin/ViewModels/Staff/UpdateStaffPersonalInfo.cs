using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class UpdateStaffPersonalInfo: PutStaffPersonalInfo
    {
        public UpdateStaffPersonalInfo()
        {
            WorkTeams = new List<SelectListItem>();
        }

        public List<SelectListItem> WorkTeams { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile ProfilePixFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile DrivingLicenseFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile RightToWorkFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile DbsFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile NiFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile SelfPyeFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile CoverLetterFile { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg", ".pdf" })]
        public IFormFile CvFile { get; set; }

    }
}
