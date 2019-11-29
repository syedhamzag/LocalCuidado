using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
    public class PostClientRota
    {
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client RotaType is required")]
        public int ClientRotaTypeId { get; set; }
    }
}
