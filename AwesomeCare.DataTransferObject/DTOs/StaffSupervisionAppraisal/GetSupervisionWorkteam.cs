using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffSupervision
{
    public class GetSupervisionWorkteam
    {
        public int SupervisionWorkteamId { get; set; }
        public int SupervisionId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public string StaffName { get; set; }

    }
}
