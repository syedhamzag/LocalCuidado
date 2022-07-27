using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.ClientPerformance
{
    public class CreatePerformanceIndicator
    {
        public List<SelectListItem> HeadingList { get; set; } = new List<SelectListItem>();
        public int PerformanceIndicatorId { get; set; }

        public string Heading { get; set; }
        public int HeadingId { get; set; }
        public int TaskCount { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; } = new List<GetBaseRecordItem>();
        public List<GetClientPerformanceIndicatorTask> Tasks { get; set; } = new List<GetClientPerformanceIndicatorTask>();
    }
}
