using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class MedicationManufacturer:Base.BaseModel
    {
        public int MedicationManufacturerId { get; set; }
        public string Manufacturer { get; set; }
    }
}
