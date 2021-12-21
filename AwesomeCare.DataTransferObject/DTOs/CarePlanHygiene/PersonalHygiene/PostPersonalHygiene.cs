using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene
{
    public class PostPersonalHygiene
    {
        public int HygieneId { get; set; }
        public int ClientId { get; set; }
        public int Cleaning { get; set; }
        public int CleaningTools { get; set; }
        public int WhoClean { get; set; }
        public int DesiredCleaning { get; set; }
        public int CleaningFreq { get; set; }
        public int GeneralAppliance { get; set; }
        public int DirtyLaundry { get; set; }
        public int DryLaundry { get; set; }
        public int WashingMachine { get; set; }
        public int Ironing { get; set; }
        public string LaundryGuide { get; set; }
        public string LaundrySupport { get; set; }
    }
}
