using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffCompetenceTest : Base.BaseModel
    {
        public StaffCompetenceTest()
        {
            StaffCompetenceTestTask = new HashSet<StaffCompetenceTestTask>();
        }
        public int StaffCompetenceTestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual ICollection<StaffCompetenceTestTask> StaffCompetenceTestTask { get; set; }
    }
}
