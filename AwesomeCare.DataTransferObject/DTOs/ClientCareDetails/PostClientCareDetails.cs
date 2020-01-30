using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetails
{
   public class PostClientCareDetails
    {
        [Required(ErrorMessage = "Please provide Task")]
        public int ClientCareDetailsTaskId { get; set; }
       // [Required(ErrorMessage ="Please provide Client")]
        public int ClientId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(250)]
        public string Risk { get; set; }
        [MaxLength(250)]
        public string Mitigation { get; set; }
        [MaxLength(250)]
        public string Location { get; set; }
        [MaxLength(250)]
        public string Remark { get; set; }
    }
}
