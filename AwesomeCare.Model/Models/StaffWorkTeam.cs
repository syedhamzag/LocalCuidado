using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class StaffWorkTeam
    {
        public StaffWorkTeam()
        {
            StaffPersonalInfo = new HashSet<StaffPersonalInfo>();
        }
        public int StaffWorkTeamId { get; set; }
        public string WorkTeam { get; set; }

        public virtual ICollection<StaffPersonalInfo> StaffPersonalInfo { get; set; }
    }
}
