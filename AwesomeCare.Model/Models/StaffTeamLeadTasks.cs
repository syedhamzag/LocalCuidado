using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffTeamLeadTasks : Base.BaseModel
    {
        public int TeamLeadTaskId { get; set; }
        public int TeamLeadId { get; set; }
        public int Title { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }

        public virtual StaffTeamLead StaffTeamLead { get; set; }
    }
}
