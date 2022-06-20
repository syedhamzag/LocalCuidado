using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
    public class GroupLiveMed
    {
        public string StaffName { get; set; }
        public List<MedTracker> medTrackers { get; set; }  = new List<MedTracker>();
    }
}
