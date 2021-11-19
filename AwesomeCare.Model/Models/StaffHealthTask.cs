using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffHealthTask : Base.BaseModel
    {
        public StaffHealthTask()
        {
        }
        public int StaffHealthTaskId { get; set; }
        public int StaffHealthId { get; set; }
        public int Title { get; set; }
        public int Answer { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }
        public int Score { get; set; }

        public virtual StaffHealth StaffHealth{ get; set; }
    }
}
