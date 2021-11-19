using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest
{
    public class GetStaffCompetenceTest : BaseDTO
    {
        public GetStaffCompetenceTest()
        {
            GetStaffCompetenceTestTask = new List<GetStaffCompetenceTestTask>();
        }
        public int StaffCompetenceTestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<GetStaffCompetenceTestTask> GetStaffCompetenceTestTask { get; set; }
    }
}
