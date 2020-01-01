using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientCareDetailsHeading:Base.BaseModel
    {
        public int ClientCareDetailsHeadingId { get; set; }
        public string Heading { get; set; }
    }
}
