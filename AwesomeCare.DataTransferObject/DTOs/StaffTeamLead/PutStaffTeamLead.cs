using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTeamLead
{
    public class PutStaffTeamLead : BaseDTO
    {
        public PutStaffTeamLead()
        {
            PutStaffTeamLeadTasks = new List<PutStaffTeamLeadTasks>();
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
        public List<PutStaffTeamLeadTasks> PutStaffTeamLeadTasks { get; set; }
    }
}
