using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PerformanceIndicatorTask : Base.BaseModel
    {
        public PerformanceIndicatorTask()
        {
        }
        public int PerformanceIndicatorTaskId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public int Title { get; set; }
        public int Score { get; set; }

        public virtual PerformanceIndicator PerformanceIndicator { get; set; }
    }
}
