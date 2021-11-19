using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffHealth
{
    public class GetStaffHealth : BaseDTO
    {
        public GetStaffHealth()
        {
            GetStaffHealthTask = new List<GetStaffHealthTask>();
        }
        public int StaffHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<GetStaffHealthTask> GetStaffHealthTask { get; set; }
    }
}
