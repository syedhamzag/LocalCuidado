using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.DutyOnCall
{
    public class CreateDutyOnCall
    {
        public CreateDutyOnCall()
        {
            Staffs = new List<SelectListItem>();
        }
        public List<SelectListItem> Staffs { get; set; }
        
        
        
        public int DutyOnCallId { get; set; }
        public int ClientId { get; set; }
        public string Subject { get; set; }
        public List<int> PersonResponsible { get; set; }
        public DateTime DateOfCall { get; set; }
        public List<int> PersonToAct { get; set; }
        public DateTime TimeOfCall { get; set; }
        public int Status { get; set; }
        public string DetailsOfIncident { get; set; }
        public string ActionTaken { get; set; }
        public int Priority { get; set; }
        public string RefNo { get; set; }
        public int TypeOfDutyCall { get; set; }

        public string ClientInitial { get; set; }
        public DateTime DateOfIncident { get; set; }
        public int TypeOfIncident { get; set; }
        public string ReportedBy { get; set; }
        public int TelephoneToCall { get; set; }
        public int PositionOfReporting { get; set; }
        
        
        
        
        public string DetailsRequired { get; set; }
        
        
        public string Remarks { get; set; }
        public bool NotifyPerson { get; set; }
        public bool StaffBlacklisted { get; set; }
        public bool NotifyStaffInvolved { get; set; }
        public string Attachment { get; set; }

        public string StatusName { get; set; }
        public string ClientName { get; set; }

        
        
    }
}
