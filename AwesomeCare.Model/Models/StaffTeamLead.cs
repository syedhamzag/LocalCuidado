using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffTeamLead : Base.BaseModel
    {
        public StaffTeamLead()
        {
            StaffTeamLeadTasks = new HashSet<StaffTeamLeadTasks>();
        }
        public int TeamLeadId { get; set; }
        public DateTime Date { get; set; }
        public int Rota { get; set; }
        public int ClientInvolved { get; set; }
        public int StaffInvolved { get; set; }
        public string DidYouObserved { get; set; }
        public string DidYouDo { get; set; }
        public string OfficeToDo { get; set; }
        public int StaffStoppedWorking { get; set; }

        public virtual ICollection<StaffTeamLeadTasks> StaffTeamLeadTasks { get; set; }
        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }

    }
}
