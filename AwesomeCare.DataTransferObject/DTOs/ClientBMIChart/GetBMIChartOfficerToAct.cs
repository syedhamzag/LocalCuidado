﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class GetBMIChartOfficerToAct
    {
        public int BMIChartOfficerToActId { get; set; }
        public int BMIChartId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public string StaffName { get; set; }

    }
}
