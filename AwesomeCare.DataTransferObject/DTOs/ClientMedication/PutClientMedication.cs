using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedication
{
   public class PutClientMedication
    {
        public PutClientMedication()
        {
            ClientMedicationDay = new List<PutClientMedicationDay>();
        }
        [Required]
        public int ClientMedicationId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [Required]
        public int MedicationManufacturerId { get; set; }
        [Required]
        [MaxLength(15)]
        public string ExpiryDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Dossage { get; set; }
        [Required]
        [MaxLength(50)]
        public string Frequency { get; set; }
        [Required]
        public int Gap_Hour { get; set; }
        [Required]
        [MaxLength(50)]
        public string Route { get; set; }
        [Required]
        [MaxLength(15)]
        public string StartDate { get; set; }
        [Required]
        [MaxLength(15)]
        public string StopDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        [Required]
        [MaxLength(250)]
        public string Remark { get; set; }

        public List<PutClientMedicationDay> ClientMedicationDay { get; set; }
    }
}
