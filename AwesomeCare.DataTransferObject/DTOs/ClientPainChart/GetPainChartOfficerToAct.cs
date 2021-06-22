using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPainChart
{
    public class GetPainChartOfficerToAct
    {
        public int PainChartOfficerToActId { get; set; }
        public int PainChartId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
