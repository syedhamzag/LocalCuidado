using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffInterview : Base.BaseModel
    {
        public StaffInterview()
        {
            StaffInterviewTask = new HashSet<StaffInterviewTask>();
        }
        public int StaffInterviewId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual ICollection<StaffInterviewTask> StaffInterviewTask { get; set; }
    }
}
