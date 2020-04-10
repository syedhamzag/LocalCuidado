using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer
{
   public class GetMedicationManufacturer:BaseDTO
    {
        public int MedicationManufacturerId { get; set; }
        public string Manufacturer { get; set; }
    }
}
