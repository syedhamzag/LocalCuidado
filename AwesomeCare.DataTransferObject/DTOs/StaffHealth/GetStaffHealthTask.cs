using System;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffHealth
{
    public class GetStaffHealthTask : BaseDTO
    {
        public int StaffHealthTaskId { get; set; }
        public int StaffHealthId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point {get; set;}
        public int Score {get; set;}
        public string TitleName { get; set; }
        public string AnswerName { get; set; }
    }
}
