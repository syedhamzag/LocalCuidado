using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod
{
   public class PutClientMedicationPeriod
    {
        [Required]
        public int ClientMedicationPeriodId { get; set; }
        [Required]
        public int ClientRotaTypeId { get; set; }
        [Required]
        public int ClientMedicationDayId { get; set; }

    }
}
