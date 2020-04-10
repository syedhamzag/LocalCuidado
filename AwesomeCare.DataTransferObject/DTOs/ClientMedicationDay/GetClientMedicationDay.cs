using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay
{
  public  class GetClientMedicationDay
    {
        public GetClientMedicationDay()
        {
            ClientMedicationPeriod = new List<GetClientMedicationPeriod>();
        }
        public int ClientMedicationDayId { get; set; }
        public int ClientMedicationId { get; set; }
        public int RotaDayofWeekId { get; set; }
        public string DayOfWeek { get; set; }

        public List<GetClientMedicationPeriod> ClientMedicationPeriod { get; set; }
    }
}
