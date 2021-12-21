using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Personal
    {
        public int PersonalId { get; set; }
        public int PersonalDetailId { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public int Smoking { get; set; }
        public int DNR { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
