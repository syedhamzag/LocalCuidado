using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientRota
    {
        public int ClientRotaId { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// AM, TEA, LUNCH e.t.c.
        /// </summary>
        public int ClientRotaTypeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientRotaType ClientRotaType { get; set; }
    }
}
