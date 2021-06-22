using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Resources
{
    public class PutResources
    {
        public int ResourcesId { get; set; }
        public DateTime Date { get; set; }
        public int PublishTo { get; set; }
        public string Heading { get; set; }
        public string Note { get; set; }
        public string PublishBy { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }

    }
}