using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffHealth
{
    public class PutStaffHealth:BaseDTO
    {
        public PutStaffHealth()
        {
            PutStaffHealthTask = new List<PutStaffHealthTask>();
        }
        public int StaffHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PutStaffHealthTask> PutStaffHealthTask { get; set; }
    }
}
