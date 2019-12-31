using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RegulatoryContact
{
   public class PostClientRegulatoryContact
    {       
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
       
        public string Evidence { get; set; }
    }
}
