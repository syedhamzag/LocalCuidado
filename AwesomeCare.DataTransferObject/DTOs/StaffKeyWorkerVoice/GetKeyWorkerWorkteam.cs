using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker
{
    public class GetKeyWorkerWorkteam
    {
        public int KeyWorkerWorkteamId { get; set; }
        public int KeyWorkerId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public string StaffName { get; set; }

    }
}
