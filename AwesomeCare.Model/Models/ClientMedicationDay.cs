using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientMedicationDay
    {
        public int ClientMedicationDayId { get; set; }
        public int ClientMedicationId { get; set; }
        public int RotaDayofWeekId { get; set; }


        public virtual RotaDayofWeek RotaDayofWeek { get; set; }
        public virtual ClientMedication ClientMedication { get; set; }
        public virtual ICollection<ClientMedicationPeriod> ClientMedicationPeriod { get; set; }
    }
}
