using AwesomeCare.DataTransferObject.DTOs.IncidentReporting;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.Admin.ViewModels.IncidentReport
{
    public class PostIncidentReportViewModel : PostIncidentReport
    {
        public PostIncidentReportViewModel()
        {
            Staffs = new List<SelectListItem>();
            Clients = new List<SelectListItem>();
        }

        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Clients { get; set; }

        [Display(Name = "Attachment, if any")]
        [DataType(DataType.Upload)]
        public IFormFile UploadAttachment { get; set; }
    }
}
