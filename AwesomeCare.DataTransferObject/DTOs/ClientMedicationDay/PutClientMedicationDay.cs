using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay
{
   public class PutClientMedicationDay
    {
        public PutClientMedicationDay()
        {
            ClientMedicationPeriod = new List<PutClientMedicationPeriod>();
        }
        [Required]
        public int ClientMedicationDayId { get; set; }
        [Required]
        public int ClientMedicationId { get; set; }
        [Required]
        public int RotaDayofWeekId { get; set; }

        public List<PutClientMedicationPeriod> ClientMedicationPeriod { get; set; }
    }
}
