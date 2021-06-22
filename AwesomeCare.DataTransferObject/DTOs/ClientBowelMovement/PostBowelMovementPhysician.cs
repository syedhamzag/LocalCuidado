using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement
{
    public class PostBowelMovementPhysician
    {
        public int BowelMovementPhysicianId { get; set; }
        public int BowelMovementId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
