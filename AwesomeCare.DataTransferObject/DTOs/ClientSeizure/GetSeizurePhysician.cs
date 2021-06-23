using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientSeizure
{
    public class GetSeizurePhysician
    {
        public int SeizurePhysicianId { get; set; }
        public int SeizureId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
