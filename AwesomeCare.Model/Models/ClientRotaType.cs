using System;
using System.Collections.Generic;
using System.Text;
using AwesomeCare.Model.Models.Base;
namespace AwesomeCare.Model.Models
{
  public  class ClientRotaType:BaseModel
    {
        public ClientRotaType()
        {
            ClientRota = new HashSet<ClientRota>();
            ClientMedicationPeriod = new HashSet<ClientMedicationPeriod>();
            StaffRotaPeriods = new HashSet<StaffRotaPeriod>();
        }
        public int ClientRotaTypeId { get; set; }
        public string RotaType { get; set; }

        public virtual ICollection<ClientRota> ClientRota { get; set; }
        public virtual ICollection<ClientMedicationPeriod> ClientMedicationPeriod { get; set; }
        public virtual ICollection<StaffRotaPeriod> StaffRotaPeriods { get; set; }
    }
}
