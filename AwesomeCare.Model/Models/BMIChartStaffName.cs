using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BMIChartStaffName
    {
        public int BMIChartStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BMIChartId { get; set; }

        public virtual ClientBMIChart BMIChart { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
