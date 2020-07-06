using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.IncidentReport
{
    public class PostStaffIncidentReportViewModel: PostReportStaff
    {
        public PostStaffIncidentReportViewModel()
        {
            Staffs = new List<SelectListItem>();
            Clients = new List<SelectListItem>();
        }

        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Clients { get; set; }

        [Display(Name = "Attachment, if any")]
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile UploadAttachment { get; set; }
    }
}
