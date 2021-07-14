using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ConsentData
    {
        public int DataId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
