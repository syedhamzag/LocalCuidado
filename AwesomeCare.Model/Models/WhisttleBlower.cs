using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class WhisttleBlower
    {
        public int WhisttleBlowerId { get; set; }
        public DateTime Date { get; set; }
        public int UserName { get; set; }
        public string StaffName { get; set; }
        public string IncidentDate { get; set; }
        public string Happening { get; set; }
        public string Evidence { get; set; }
        public int Witness { get; set; }
        public int LikeCalling { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
    }
}
