using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BowelMovementPhysician
    {
        public int BowelMovementPhysicianId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BowelMovementId { get; set; }

        public virtual ClientBowelMovement BowelMovement { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
