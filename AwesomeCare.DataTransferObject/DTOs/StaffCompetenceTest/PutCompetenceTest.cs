using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest
{
    public class PutStaffCompetenceTest:BaseDTO
    {
        public PutStaffCompetenceTest()
        {
            PutStaffCompetenceTestTask = new List<PutStaffCompetenceTestTask>();
        }
        public int StaffCompetenceTestId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PutStaffCompetenceTestTask> PutStaffCompetenceTestTask { get; set; }
    }
}
