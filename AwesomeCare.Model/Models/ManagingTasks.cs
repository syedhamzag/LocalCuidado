using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ManagingTasks
    {
        public int TaskId {get; set;}
        public int ClientId { get; set; }
        public int Task { get; set; }
        public string Help { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
    }
}
