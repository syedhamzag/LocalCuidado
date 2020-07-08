using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
    public class PostClientServiceDetailItem
    {
        [Required]
        public int ClientServiceDetailId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
