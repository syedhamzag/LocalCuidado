using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading
{
   public class PutClientCareDetailsHeading:BaseDTO
    {
        public int ClientCareDetailsHeadingId { get; set; }
        public string Heading { get; set; }
    }
}
