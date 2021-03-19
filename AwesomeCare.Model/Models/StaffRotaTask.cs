using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    /// <summary>
    /// For Recording the Rota Tasks Performed by Staff
    /// </summary>
   public class StaffRotaTask
    {
        public int StaffRotaTaskId { get; set; }
        public int StaffRotaPeriodId { get; set; }
        public int RotaTaskId { get; set; }
        public bool IsGiven { get; set; }

        public virtual StaffRotaPeriod StaffRotaPeriod { get; set; }
    }
}
