using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading
{
   public class PostClientCareDetailsHeadingTask
    {
        [Required]
        public string Task { get; set; }
    }
}
