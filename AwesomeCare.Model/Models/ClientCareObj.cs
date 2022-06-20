using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientCareObj
    {
        public int CareObjId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<CareObjPersonToAct> PersonToAct { get; set; }
    }
}
