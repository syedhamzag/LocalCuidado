using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTeamLead
{
    public class GetStaffTeamLead : BaseDTO
    {
        public GetStaffTeamLead()
        {
            GetStaffTeamLeadTasks = new List<GetStaffTeamLeadTasks>();
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
        public List<GetStaffTeamLeadTasks> GetStaffTeamLeadTasks { get; set; }
    }

}
