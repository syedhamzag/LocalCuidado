using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffHealth : Base.BaseModel
    {
        public StaffHealth()
        {
            StaffHealthTask = new HashSet<StaffHealthTask>();
        }
        public int StaffHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual ICollection<StaffHealthTask> StaffHealthTask { get; set; }
    }
}
