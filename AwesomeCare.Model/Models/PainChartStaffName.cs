using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class PainChartStaffName
    {
        public int PainChartStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int PainChartId { get; set; }

        public virtual ClientPainChart PainChart { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}