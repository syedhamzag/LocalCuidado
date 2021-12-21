using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Name { get; set; }
        public int Type { get; set; }
        public int Location { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public string Attachment { get; set; }
        public int Status { get; set; }
        public int PersonToAct { get; set; } //Staff From DataBase

        public virtual StaffPersonalInfo Staff { get; set; }
        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
