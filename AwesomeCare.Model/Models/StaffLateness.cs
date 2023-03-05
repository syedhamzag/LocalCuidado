using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Model.Models
{
    public class StaffLateness : Base.BaseModel
    {
        public int StaffLatenessId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int SN { get; set; }
        public DateTime Date { get; set; }
        public int Rota { get; set; }
        public DateTime TimeCritical { get; set; }
        public string Reason { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
