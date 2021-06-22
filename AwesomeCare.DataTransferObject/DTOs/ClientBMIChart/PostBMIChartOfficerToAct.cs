using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class PostBMIChartOfficerToAct
    {
        public int BMIChartOfficerToActId { get; set; }
        public int BMIChartId { get; set; }
        public int StaffPersonalInfoId { get; set; }

    }
}
