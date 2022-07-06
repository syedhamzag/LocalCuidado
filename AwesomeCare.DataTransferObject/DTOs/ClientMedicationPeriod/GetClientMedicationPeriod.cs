using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod
{
   public class GetClientMedicationPeriod
    {
        public int ClientMedicationPeriodId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public int ClientMedicationDayId { get; set; }
        public int RotaId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string RotaType { get; set; }
    }
}
