using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator
{
    public class GetClientPerformanceIndicator
    {
        public int PerformanceIndicatorId { get; set; }
        public string Heading { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }
        public int ClientId { get; set; }
        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<GetClientPerformanceIndicatorTask> GetClientPerformanceIndicatorTask { get; set; } = new List<GetClientPerformanceIndicatorTask>();
    }
}
