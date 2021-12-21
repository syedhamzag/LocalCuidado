using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffCompetenceTestTask : Base.BaseModel
    {
        public StaffCompetenceTestTask()
        {
        }

        public int StaffCompetenceTestTaskId { get; set; }
        public int StaffCompetenceTestId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }
        public int Score { get; set; }

        public virtual StaffCompetenceTest StaffCompetenceTest { get; set; }
    }
}
