using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPainChart
{
    public class PutClientPainChart
    {
        public PutClientPainChart()
        {
            OfficerToAct = new List<PutPainChartOfficerToAct>();
            Physician = new List<PutPainChartPhysician>();
            StaffName = new List<PutPainChartStaffName>();
        }

        public List<PutPainChartOfficerToAct> OfficerToAct { get; set; }
        public List<PutPainChartPhysician> Physician { get; set; }
        public List<PutPainChartStaffName> StaffName { get; set; }

        public int PainChartId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Type { get; set; }
        public string TypeAttach { get; set; }
        public int Location { get; set; }
        public string LocationAttach { get; set; }
        public int PainLvl { get; set; }
        public string Comment { get; set; }
        public int StatusImage { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public string StatusAttach { get; set; }
    }
}
