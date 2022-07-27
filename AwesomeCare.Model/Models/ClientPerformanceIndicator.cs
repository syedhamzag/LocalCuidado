using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientPerformanceIndicator
    {
        public int PerformanceIndicatorId { get; set; }
        public int ClientId { get; set; }
        public string Heading { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int Rating { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<ClientPerformanceIndicatorTask> ClientPerformanceIndicatorTask { get; set; } = new HashSet<ClientPerformanceIndicatorTask>();
    }
}
