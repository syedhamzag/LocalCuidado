using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientService
{
    public class GetServiceOfficerToAct
    {
        public int ServiceOfficerToActId { get; set; }
        public int ServiceId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
