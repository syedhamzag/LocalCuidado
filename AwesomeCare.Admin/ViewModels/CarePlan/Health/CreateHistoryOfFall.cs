using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.Health
{
    public class CreateHistoryOfFall
    {
        public int HistoryId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public string Cause { get; set; }
        public string Prevention { get; set; }
    }
}
