using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax
{
    public class GetStaffTax : BaseDTO
    {
        public int StaffTaxId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public decimal Tax { get; set; }
        public Decimal NI { get; set; }
        public string Remarks { get; set; }

    }
}
