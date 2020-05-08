using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedication
{
   public class GetClientMedication
    {
        public GetClientMedication()
        {
            ClientMedicationDay = new List<GetClientMedicationDay>();
        }
        public int ClientMedicationId { get; set; }
        public int ClientId { get; set; }
        public int MedicationId { get; set; }
        public int MedicationManufacturerId { get; set; }
        public string Medication { get; set; }
        [Display(Name = "Manufacturer")]
        public string MedicationManufacturer { get; set; }
        public string ExpiryDate { get; set; }
        public string Dossage { get; set; }
        public string Frequency { get; set; }
        [Display(Name = "Gap (Hours)")]
        public int Gap_Hour { get; set; }
        public string Route { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }

        public List<GetClientMedicationDay> ClientMedicationDay { get; set; }
    }
}
