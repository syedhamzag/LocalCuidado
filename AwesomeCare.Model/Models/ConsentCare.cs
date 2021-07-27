using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ConsentCare
    {
        public int CareId { get; set; }
        public int ClientId { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }

        public virtual Client Client { get; set; }
    }
}
