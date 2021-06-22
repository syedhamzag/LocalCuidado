using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement
{
    public class GetBowelMovementOfficerToAct
    {
        public int BowelMovementOfficerToActId { get; set; }
        public int BowelMovementId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
