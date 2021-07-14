using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment
{
    public class PutEquipment
    {
        public int PersonalDetailId { get; set; }
        public int EquipmentId { get; set; }
        public int Name { get; set; }
        public int StaffId { get; set; }
        public int Type { get; set; }
        public int Location { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public string Attachment { get; set; }
        public int Status { get; set; }
        public int PersonToAct { get; set; }
    }
}
