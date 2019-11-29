using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class RotaTask:Base.BaseModel
    {
        public int RotaTaskId { get; set; }
        public string TaskName { get; set; }
        public string GivenAcronym { get; set; }
        public string NotGivenAcronym { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<ClientRotaTask> ClientRotaTask { get; set; }
    }
}
