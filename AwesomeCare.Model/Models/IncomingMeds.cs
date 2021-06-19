using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class IncomingMeds
    {
        public int IncomingMedsId { get; set; }
        public DateTime Date { get; set; }
        public int UserName { get; set; }
        public string StaffName { get; set; }
        public string StartDate { get; set; }
        public string ChartImage { get; set; }
        public string MedsImage { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
    }
}
