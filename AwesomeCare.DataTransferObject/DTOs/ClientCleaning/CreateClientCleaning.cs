using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCleaning
{
    public class CreateClientCleaning
    {
        public CreateClientCleaning()
        {

        }
        [Required]
        public int CleaningId { get; set; }
        [Required]
        public int NutritionId { get; set; }
        [Required]
        public int AreasAndItems { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string SafetyHazard { get; set; }
        [Required]
        public string LocationOfItem { get; set; }
        [Required]
        public string DescOfItem { get; set; }
        [Required]
        public DateTime MinuteAlloted { get; set; }
        [Required]
        public string Disposal { get; set; }
        [Required]
        public int WhereToGet { get; set; }
        public string WhereToKeep { get; set; }
        [Required]
        public string SEEVIDEO { get; set; }
        public string Image { get; set; }
        [Required]
        public string DAYOFCLEANING { get; set; }
        [Required]
        public DateTime DATEFROM { get; set; }
        [Required]
        public DateTime DATETO { get; set; }
        [Required]
        public int STAFFId { get; set; }
    }
}
