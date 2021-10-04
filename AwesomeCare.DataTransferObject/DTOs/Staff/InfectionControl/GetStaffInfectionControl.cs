using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl
{
    public class GetStaffInfectionControl
    {
        public int InfectionId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public int Type { get; set; }
        public string Guideline { get; set; }
        public DateTime TestDate { get; set; }
        public int VaccStatus { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

    }
}
