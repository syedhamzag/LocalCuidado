using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay
{
   public class PostClientMedicationDay
    {
        public PostClientMedicationDay()
        {
            ClientMedicationPeriod = new List<PostClientMedicationPeriod>();
        }
        [Required]
        public int ClientMedicationId { get; set; }
        [Required]
        public int RotaDayofWeekId { get; set; }

        public List<PostClientMedicationPeriod> ClientMedicationPeriod { get; set; }
    }
}
