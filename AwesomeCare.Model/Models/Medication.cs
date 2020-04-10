using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class Medication:Base.BaseModel
    {
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string Strength { get; set; }
    }
}
