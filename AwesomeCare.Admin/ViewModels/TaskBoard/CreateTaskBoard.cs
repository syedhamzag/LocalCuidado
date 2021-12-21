using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.TaskBoard
{
    public class CreateTaskBoard
    {
        public CreateTaskBoard()
        {
            StaffList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        
        public IFormFile Image { get; set; }

        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int AssignedBy { get; set; }
        public string TaskImage { get; set; }
        public string Attachment { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public List<int> AssignedTo { get; set; }
        public string StaffName { get; set; }
        public string StatusName { get; set; }

        public ICollection<SelectListItem> StaffList { get; set; }
    }
}
