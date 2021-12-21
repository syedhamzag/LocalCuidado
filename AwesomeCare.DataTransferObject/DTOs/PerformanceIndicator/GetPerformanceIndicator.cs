using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator
{
    public class GetPerformanceIndicator : BaseDTO
    {
        public GetPerformanceIndicator()
        {
            GetPerformanceIndicatorTask = new List<GetPerformanceIndicatorTask>();
        }
        public int PerformanceIndicatorId { get; set; }
        public string Heading { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<GetPerformanceIndicatorTask> GetPerformanceIndicatorTask { get; set; }
    }
}
