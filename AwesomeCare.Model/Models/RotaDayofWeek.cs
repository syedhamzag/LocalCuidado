using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class RotaDayofWeek:Base.BaseModel
    {
        public int RotaDayofWeekId { get; set; }
        public string DayofWeek { get; set; }

        public virtual ICollection<ClientRotaDays> ClientRotaDays { get; set; }
    }
}
