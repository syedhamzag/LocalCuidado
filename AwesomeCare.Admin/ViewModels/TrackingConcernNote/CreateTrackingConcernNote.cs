using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.TrackingConcernNote
{
    public class CreateTrackingConcernNote
    {
        public CreateTrackingConcernNote()
        {
            Staffs = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        public IFormFile Attach { get; set; }

        public int Ref { get; set; }
        public DateTime Date { get; set; }
        public string ConcernNote { get; set; }
        public string ActionRequired { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime ExpectedDeadline { get; set; }
        public int StaffNotify { get; set; }
        public int ManagerCopied { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }
        public List<SelectListItem> Staffs { get; set; }
        public List<int> Manager { get; set; }
        public List<int> Staff { get; set; }
        public string StatusName { get; set; }
    }
}
