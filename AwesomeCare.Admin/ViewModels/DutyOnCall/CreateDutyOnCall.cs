using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.DutyOnCall
{
    public class CreateDutyOnCall
    {
        [DataType(DataType.Upload)]
        public IFormFile Attach { get; set; }

        public List<SelectListItem> Staffs { get; set; } = new List<SelectListItem>(); 
        public List<SelectListItem> ListItems { get; set; } = new List<SelectListItem>{
                new SelectListItem("Yes","true"),
                new SelectListItem("No","false")
            };


        public int DutyOnCallId { get; set; }
        public int ClientId { get; set; }
        public string Subject { get; set; }
        public List<int> PersonResponsible { get; set; }
        public string PersonResponsibleName { get; set; } 
        public DateTime DateOfCall { get; set; }
        public List<int> PersonToAct { get; set; }
        public string PersonToActName { get; set; } 
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


        public int NotificationStatus { get; set; }


        public string DetailsRequired { get; set; }
        
        
        public string Remarks { get; set; }
        public bool NotifyPerson { get; set; }
        public bool StaffBlacklisted { get; set; }
        public bool NotifyStaffInvolved { get; set; }
        public string Attachment { get; set; }
        public string StatusName { get; set; }
        public string ClientName { get; set; }
        public string IdNumber { get; set; }
        public string DOB { get; set; }
        public string PriorityName { get; set; }
        public string NotificationStatusName { get; set; }
        public string TypeOfIncidentName { get; set; }
        public string TelephoneToCallName { get; set; }
        public string PositionOfReportingName { get; set; }
        public string TypeOfDutyCallName { get; set; }


    }
}
