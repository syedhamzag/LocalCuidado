using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator
{
    public class GetPerformanceIndicatorTask : BaseDTO
    {
        public int PerformanceIndicatorTaskId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public int Title { get; set; }
        public int Score { get; set; }
        public string TitleName { get; set; }
    }
}
