using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientKeyWorker
{
    public class GetKeyWorkerOfficerToAct
    {
        public int KeyWorkerOfficerToActId { get; set; }
        public int KeyWorkerId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
