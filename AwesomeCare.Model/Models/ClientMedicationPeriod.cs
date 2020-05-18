using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientMedicationPeriod
    {
        public int ClientMedicationPeriodId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public int ClientMedicationDayId { get; set; }


        public virtual ClientMedicationDay ClientMedicationDay { get; set; }
        public virtual ClientRotaType ClientRotaType { get; set; }
    }
}
