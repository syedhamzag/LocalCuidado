﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PutHealthTask2
    {
        public int HealthTask2Id { get; set; }
        public int BestId { get; set; }
        public int Heading2Id { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
