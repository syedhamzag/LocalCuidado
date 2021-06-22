using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class PutBMIChartStaffName
    {
        public int BMIChartOfficerToActId { get; set; }
        public int BMIChartId { get; set; }
        public int StaffPersonalInfoId { get; set; }

    }
}
