using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientComplain
{
    public class GetComplainStaffName
    {
        public int ComplainStaffNameId { get; set; }
        public int ComplainId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
