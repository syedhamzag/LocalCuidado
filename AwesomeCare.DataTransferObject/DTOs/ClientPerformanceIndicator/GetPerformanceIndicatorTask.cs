﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator
{
    public class GetClientPerformanceIndicatorTask 
    {
        public int PerformanceIndicatorTaskId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public int Title { get; set; }
        public int Score { get; set; }
        public string TitleName { get; set; }
    }
}
