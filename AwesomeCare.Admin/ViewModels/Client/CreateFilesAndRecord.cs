using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateFilesAndRecord
    {
        public CreateFilesAndRecord()
        {
            StaffList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)] 
        public IFormFile Attach { get; set; }
        public int FilesAndRecordId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string Subject { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }

        public string StaffName { get; set; }
        public string ClientName { get; set; }
        public List<SelectListItem> StaffList { get; set; }
    }
}
