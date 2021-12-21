using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HistoryOfFall
    {
        public int HistoryId { get; set; }
        public int ClientId { get; set; }
        public string Details { get; set; }
        public DateTime Date{ get; set; }
        public string Cause { get; set; }
        public string Prevention{ get; set; }

        public virtual Client Client { get; set; }
    }
}
