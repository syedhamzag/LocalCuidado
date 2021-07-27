using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ClientId { get; set; }
        public DateTime CP_PreDate { get; set; }
        public DateTime CP_ReviewDate { get; set; }
        public DateTime RA_PreDate { get; set; }
        public DateTime RA_ReviewDate { get; set; }

        public virtual Client Client { get; set; }
    }
}
