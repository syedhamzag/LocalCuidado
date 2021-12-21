using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class EyeHealthPhysician
    {
        public int EyeHealthPhysicianId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int EyeHealthId { get; set; }

        public virtual ClientEyeHealthMonitoring EyeHealth { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
