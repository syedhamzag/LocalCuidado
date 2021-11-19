using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShadowing
{
    public class PutStaffShadowingTask : BaseDTO
    {
        public int StaffShadowingTaskId { get; set; }
        public int StaffShadowingId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point {get; set;}
        public int Score {get; set;}
    }
}
