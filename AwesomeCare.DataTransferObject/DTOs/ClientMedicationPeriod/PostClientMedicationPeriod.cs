using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod
{
    public class PostClientMedicationPeriod
    {
        [Required]
        public int ClientRotaTypeId { get; set; }
        [Required]
        public int ClientMedicationDayId { get; set; }
    }
}
