using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
   public class GetMedication:BaseDTO
    {
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string Strength { get; set; }
    }
}
