using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientShopping
{
    public class CreateClientShopping
    {
        public CreateClientShopping()
        {
        
        }
        [Required]
        public int ShoppingId { get; set; }
        [Required]
        public int NutritionId { get; set; }
        [Required]
        public string MeansOfPurchase { get; set; }
        [Required]
        public string LocationOfPurchase { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Image { get; set; }
        [Required]
        public string DAYOFSHOPPING { get; set; }
        [Required]
        public DateTime DATEFROM { get; set; }
        [Required]
        public DateTime DATETO { get; set; }
        [Required]
        public int STAFFId { get; set; }
    }
}
