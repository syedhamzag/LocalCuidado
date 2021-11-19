using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShadowing
{
    public class PutStaffShadowing:BaseDTO
    {
        public PutStaffShadowing()
        {
            PutStaffShadowingTask = new List<PutStaffShadowingTask>();
        }
        public int StaffShadowingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PutStaffShadowingTask> PutStaffShadowingTask { get; set; }
    }
}
