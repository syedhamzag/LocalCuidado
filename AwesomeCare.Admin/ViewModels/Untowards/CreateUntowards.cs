using AwesomeCare.DataTransferObject.DTOs.Untowards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Untowards
{
    public class CreateUntowards: PostUntowards
    {
        public CreateUntowards()
        {
            ActionStatuses = new List<SelectListItem>
            {
                new SelectListItem("Open","Open"),
                new SelectListItem("Close","Close")
            };

            Priorities = new List<SelectListItem>
            {
                new SelectListItem("Normal","Normal"),
                new SelectListItem("Urgent","Urgent")
            };
            YesNo = new List<SelectListItem>
            {
                new SelectListItem("No","No"),
                new SelectListItem("Yes","Yes")
            };
            HomeCareClients = new List<SelectListItem>();
            ClientInvolvingParties = new List<SelectListItem>();

            StaffsInvolved = new List<string>();
            OfficersToAct = new List<string>();
        }
        public List<SelectListItem> ActionStatuses { get; set; }
        public List<SelectListItem> Priorities { get; set; }
        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> HomeCareClients { get; set; }
        public List<SelectListItem> ClientInvolvingParties { get; set; }
        public List<SelectListItem> YesNo { get; set; }

        [Display(Name = "Staffs Involved")]
        public List<string> StaffsInvolved { get; set; }
        [Display(Name = "Officers To Act")]
        public List<string> OfficersToAct { get; set; }

        public string IsBlackListed { get; set; }
        public string HospitalEntry { get; set; }
        public string HospitalExit { get; set; }
        public string NotifyInvolvingStaff { get; set; }

        public IFormFile FileAttachment { get; set; }
    }
}
