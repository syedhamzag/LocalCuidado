using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest
{
    public class PostStaffCompetenceTest : BaseDTO
    {
        public PostStaffCompetenceTest()
        {
            PostStaffCompetenceTestTask = new List<PostStaffCompetenceTestTask>();
        }
        public int StaffCompetenceTestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PostStaffCompetenceTestTask> PostStaffCompetenceTestTask { get; set; }
    }
}
