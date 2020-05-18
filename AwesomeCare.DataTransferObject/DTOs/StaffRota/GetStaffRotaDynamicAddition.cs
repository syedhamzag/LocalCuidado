using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
    public class GetStaffRotaDynamicAddition: BaseDTO
    {
        public int StaffRotaDynamicAdditionId { get; set; }
        public string ItemName { get; set; }
    }
}
