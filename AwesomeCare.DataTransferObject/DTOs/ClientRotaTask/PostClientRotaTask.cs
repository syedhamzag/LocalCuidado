﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaTask
{
   public class PostClientRotaTask
    {
        [Required(ErrorMessage = "Rota Task is required")]
        public int RotaTaskId { get; set; }
        [Required(ErrorMessage = "Rota Day is required")]
        public int ClientRotaDaysId { get; set; }
    }
}
