using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffHealth
{
    public class PutStaffHealthTask : BaseDTO
    {
        public int StaffHealthTaskId { get; set; }
        public int StaffHealthId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point {get; set;}
        public int Score {get; set;}
    }
}
