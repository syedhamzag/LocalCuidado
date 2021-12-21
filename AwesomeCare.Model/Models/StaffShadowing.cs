using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffShadowing : Base.BaseModel
    {
        public StaffShadowing()
        {
            StaffShadowingTask = new HashSet<StaffShadowingTask>();
        }
        public int StaffShadowingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual ICollection<StaffShadowingTask> StaffShadowingTask { get; set; }
    }
}
