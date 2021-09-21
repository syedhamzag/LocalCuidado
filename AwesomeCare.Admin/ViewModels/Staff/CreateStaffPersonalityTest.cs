using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffPersonalityTest
    {
        public CreateStaffPersonalityTest()
        {
            GetStaffPersonalityTest = new List<GetStaffPersonalityTest>();
        }
        public int PersonalityCount { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TestId { get; set; }
        public string QuestionName { get; set; }
        public string AnswerName { get; set; }
        public string StaffName { get; set; }
        public List<GetStaffPersonalityTest> GetStaffPersonalityTest { get; set; }
    }
}
