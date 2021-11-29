using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PerformanceIndicator
{
    public class CreatePerformanceIndicator
    {
        public CreatePerformanceIndicator()
        {
            HeadingList = new List<SelectListItem>();
            Tasks = new List<GetPerformanceIndicatorTask>();
            baseRecordList = new List<GetBaseRecordItem>();
        }

        public List<SelectListItem> HeadingList { get; set; }
        public int PerformanceIndicatorId { get; set; }

        public string Heading { get; set; }
        public int HeadingId { get; set; }
        public int TaskCount { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<GetPerformanceIndicatorTask> Tasks { get; set; }
    }
}
