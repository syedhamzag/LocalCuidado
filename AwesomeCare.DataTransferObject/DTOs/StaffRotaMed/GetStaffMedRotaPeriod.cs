﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaMed
{
   public class GetStaffMedRotaPeriod
    {
        public int StaffRotaPeriodId { get; set; }
        public int StaffRotaId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public string RotaType { get; set; }
    }
}
