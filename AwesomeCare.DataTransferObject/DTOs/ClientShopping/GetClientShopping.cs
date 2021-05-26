using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientShopping
{
    public class GetClientShopping
    {
        public int ShoppingId { get; set; }
        public int NutritionId { get; set; }
        public string MeansOfPurchase { get; set; }
        public string LocationOfPurchase { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Image { get; set; }
        public string DAYOFSHOPPING { get; set; }
        public DateTime DATEFROM { get; set; }
        public DateTime DATETO { get; set; }
        public int STAFFId { get; set; }
    }
}
