using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer
{
   public class PutMedicationManufacturer : BaseDTO
    {
        [Required]
        public int MedicationManufacturerId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Manufacturer { get; set; }
    }
}
