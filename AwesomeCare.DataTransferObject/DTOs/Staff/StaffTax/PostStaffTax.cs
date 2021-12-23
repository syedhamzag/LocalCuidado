using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax
{
    public class PostStaffTax : BaseDTO
    {
        public int StaffTaxId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public decimal Tax { get; set; }
        public decimal NI { get; set; }
        public string Remarks { get; set; }

    }
}
