using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffTax
    {
        public int StaffTaxId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public decimal Tax { get; set; }
        public Decimal NI { get; set; }
        public string Remarks { get; set; }
        public string StaffName { get; set; }
    }
}
