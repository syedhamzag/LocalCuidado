using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaMed
{
    public class PutStaffMedRotaPeriod
    {
        public int StaffRotaId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int? ClientId { get; set; }
    }
}
