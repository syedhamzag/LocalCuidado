using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffTax : Base.BaseModel
    {
        public int StaffTaxId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public decimal Tax { get; set; }
        public decimal NI { get; set; }
        public string Remarks { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }

    }
}
