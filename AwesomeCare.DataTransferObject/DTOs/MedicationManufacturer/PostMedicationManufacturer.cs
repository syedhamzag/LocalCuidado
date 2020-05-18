using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer
{
  public  class PostMedicationManufacturer
    {
        [Required]
        [MaxLength(255)]
        public string Manufacturer { get; set; }
    }
}
