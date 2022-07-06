using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod
{
    public class PostClientMedicationPeriod
    {
        [Required]
        public int ClientRotaTypeId { get; set; }
        [Required]
        public int ClientMedicationDayId { get; set; }
        public int RotaId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
    }
}
