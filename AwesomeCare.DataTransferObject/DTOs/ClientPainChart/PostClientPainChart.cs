using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPainChart
{
    public class PostClientPainChart
    {
        public PostClientPainChart()
        {
            OfficerToAct = new List<PostPainChartOfficerToAct>();
            Physician = new List<PostPainChartPhysician>();
            StaffName = new List<PostPainChartStaffName>();
        }

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

        public List<PostPainChartOfficerToAct> OfficerToAct { get; set; }
        public List<PostPainChartPhysician> Physician { get; set; }
        public List<PostPainChartStaffName> StaffName { get; set; }
    }
}
