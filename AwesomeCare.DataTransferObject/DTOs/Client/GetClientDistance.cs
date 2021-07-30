using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Client
{
    public class GetClientDistance
    {
        public string Fullname { get; set; }
        public string Postcode { get; set; }
        public double Distance { get; set; }
    }
}
