using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientCareDetailsHeading:Base.BaseModel
    {
        public ClientCareDetailsHeading()
        {
            ClientCareDetailsTasks = new HashSet<ClientCareDetailsTask>();
        }
        public int ClientCareDetailsHeadingId { get; set; }
        public string Heading { get; set; }

        public virtual ICollection<ClientCareDetailsTask> ClientCareDetailsTasks { get; set; }
    }
}
