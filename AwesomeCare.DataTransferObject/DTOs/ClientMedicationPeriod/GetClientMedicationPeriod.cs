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

        public string RotaType { get; set; }
    }
}
