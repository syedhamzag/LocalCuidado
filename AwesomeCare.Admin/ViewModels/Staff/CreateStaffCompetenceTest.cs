using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffCompetenceTest
    {
        public CreateStaffCompetenceTest()
        {
            HeadingList = new List<SelectListItem>();
            Tasks = new List<GetStaffCompetenceTestTask>();
            baseRecordList = new List<GetBaseRecordItem>();
        }
        public List<SelectListItem> HeadingList { get; set; }
        public int StaffCompetenceTestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

        public string Heading { get; set; }
        public int HeadingId { get; set; }
        public int TaskCount { get; set; }

        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<GetStaffCompetenceTestTask> Tasks { get; set; }
    }
}
