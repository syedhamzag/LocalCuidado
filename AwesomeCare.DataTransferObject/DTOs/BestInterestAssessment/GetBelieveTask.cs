﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class GetBelieveTask
    {
        public int BelieveTaskId { get; set; }
        public int BestId { get; set; }

        public string ReasonableBelieve { get; set; }
    }
}
