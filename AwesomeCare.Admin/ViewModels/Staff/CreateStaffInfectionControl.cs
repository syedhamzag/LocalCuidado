using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffInfectionControl
    {
        public int InfectionId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public int Type { get; set; }
        public string Guideline { get; set; }
        public DateTime TestDate { get; set; }
        public int VaccStatus { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

        public string StaffName { get; set; }
        public string VaccName { get; set; }
        public string InfectionName { get; set; }
        public string TypeName { get; set; }
    }
}
