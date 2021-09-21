using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffPersonalityTest
    {
        public int TestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int Question { get; set; }
        public int Answer { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
