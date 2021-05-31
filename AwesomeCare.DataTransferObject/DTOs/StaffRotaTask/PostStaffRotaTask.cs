using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaTask
{
   public class PostStaffRotaTask
    {       
        public int StaffRotaPeriodId { get; set; }
        public int RotaTaskId { get; set; }
        public bool IsGiven { get; set; }
    }
}
