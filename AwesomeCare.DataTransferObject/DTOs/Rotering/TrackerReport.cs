using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
    public class TrackerReport
    {
        public string Rota { get; set; }
        public DateTime RotaDate { get; set; }
        public string Staff { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public double Hours { get; set; }
        public int Late { get; set; }
        public int Missed { get; set; }
        public string Remark { get; set; }
        public string LogLocation {get;set;}
        public string Period { get; set; }
    }
}
