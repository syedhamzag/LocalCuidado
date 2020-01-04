using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientCareDetailsTask:Base.BaseModel
    {
        public ClientCareDetailsTask()
        {
            ClientCareDetails = new HashSet<ClientCareDetails>();
        }
        public int ClientCareDetailsTaskId { get; set; }
        public int ClientCareDetailsHeadingId { get; set; }
        public string Task { get; set; }

        public virtual ClientCareDetailsHeading ClientCareDetailsHeading { get; set; }
        public ICollection<ClientCareDetails> ClientCareDetails { get; set; }
    }
}
