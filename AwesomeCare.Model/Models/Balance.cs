using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Balance
    {
        public int BalanceId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mobility { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
    }
}
