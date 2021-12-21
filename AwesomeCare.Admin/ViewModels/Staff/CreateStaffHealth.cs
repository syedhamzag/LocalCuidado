using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffHealth
    {
        public CreateStaffHealth()
        {
            HeadingList = new List<SelectListItem>();
            Tasks = new List<GetStaffHealthTask>();
            baseRecordList = new List<GetBaseRecordItem>();
        }
        public List<SelectListItem> HeadingList { get; set; }
        public int StaffHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

        public string Heading { get; set; }
        public int HeadingId { get; set; }
        public int TaskCount { get; set; }

        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<GetStaffHealthTask> Tasks { get; set; }
    }
}
