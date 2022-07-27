using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator
{
    public class PutPerformanceIndicator : BaseDTO
    {
        public PutPerformanceIndicator()
        {
            PutPerformanceIndicatorTask = new List<PutPerformanceIndicatorTask>();
        }
        public int PerformanceIndicatorId { get; set; }
        public string Heading { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<PutPerformanceIndicatorTask> PutPerformanceIndicatorTask { get; set; }
    }
}
