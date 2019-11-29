using System;
using System.Collections.Generic;
using System.Text;
using AwesomeCare.Model.Models.Base;
namespace AwesomeCare.Model.Models
{
  public  class ClientRotaType:BaseModel
    {
        public int ClientRotaTypeId { get; set; }
        public string RotaType { get; set; }

        public virtual ICollection<ClientRota> ClientRota { get; set; }
    }
}
