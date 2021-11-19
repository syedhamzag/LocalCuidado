using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest
{
    public class GetStaffCompetenceTestTask : BaseDTO
    {
        public int StaffCompetenceTestTaskId { get; set; }
        public int StaffCompetenceTestId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point {get; set;}
        public int Score {get; set;}
        public string TitleName { get; set; }
        public string AnswerName { get; set; }
    }
}
