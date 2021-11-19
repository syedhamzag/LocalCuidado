using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShadowing
{
    public class GetStaffShadowing : BaseDTO
    {
        public GetStaffShadowing()
        {
            GetStaffShadowingTask = new List<GetStaffShadowingTask>();
        }
        public int StaffShadowingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<GetStaffShadowingTask> GetStaffShadowingTask { get; set; }
    }
}
