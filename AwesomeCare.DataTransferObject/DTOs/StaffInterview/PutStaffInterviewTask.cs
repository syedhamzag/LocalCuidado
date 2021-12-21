using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffInterview
{
    public class PutStaffInterviewTask : BaseDTO
    {
        public int StaffInterviewTaskId { get; set; }
        public int StaffInterviewId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point {get; set;}
        public int Score {get; set;}
    }
}
