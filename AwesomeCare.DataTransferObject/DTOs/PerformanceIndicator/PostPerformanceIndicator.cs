using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator
{
    public class PostPerformanceIndicator : BaseDTO
    {
        public PostPerformanceIndicator()
        {
            PostPerformanceIndicatorTask = new List<PostPerformanceIndicatorTask>();
        }
        public int PerformanceIndicatorId { get; set; }
        public string Heading { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int Rating { get; set; }
        public string Remarks { get; set; }
        public List<PostPerformanceIndicatorTask> PostPerformanceIndicatorTask { get; set; }
    }
}
