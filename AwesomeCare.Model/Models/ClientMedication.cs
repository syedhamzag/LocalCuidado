using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientMedication
    {
       
        public int ClientMedicationId { get; set; }
        public int ClientId { get; set; }
        public int MedicationId { get; set; }
        public int MedicationManufacturerId { get; set; }

        public string ExpiryDate { get; set; }
        public string Dossage { get; set; }
        public string Frequency { get; set; }
        public int Gap_Hour { get; set; }
        public string Route { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }

        public virtual MedicationManufacturer MedicationManufacturer { get; set; }
        public virtual Medication Medication { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<ClientMedicationDay> ClientMedicationDay { get; set; }
    }
}
