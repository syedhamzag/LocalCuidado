﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement
{
    public class PutBowelMovementStaffName
    {
        public int BowelMovementOfficerToActId { get; set; }
        public int BowelMovementId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
