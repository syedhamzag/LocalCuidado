using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffTeamLead
    {
        public CreateStaffTeamLead()
        {
            GetStaffTeamLeadTasks = new List<GetStaffTeamLeadTasks>();
            baseRecordList = new List<GetBaseRecordItem>();
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
        public string ClientName { get; set; }
        public string StaffName { get; set; }
        public int TaskCount { get; set; }

        public int StaffPersonalInfoId { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<GetStaffTeamLeadTasks> GetStaffTeamLeadTasks { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; }
    }
}
