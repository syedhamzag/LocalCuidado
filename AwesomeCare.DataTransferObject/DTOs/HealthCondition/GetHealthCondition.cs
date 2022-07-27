using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.HealthCondition
{
    public class GetHealthCondition
    {
        public int HCId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
