using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class GetClientBMIChart
    {
        public GetClientBMIChart()
        {
            OfficerToAct = new List<GetBMIChartOfficerToAct>();
            Physician = new List<GetBMIChartPhysician>();
            StaffName = new List<GetBMIChartStaffName>();
        }

        public List<GetBMIChartOfficerToAct> OfficerToAct { get; set; }
        public List<GetBMIChartPhysician> Physician { get; set; }
        public List<GetBMIChartStaffName> StaffName { get; set; }

        public int BMIChartId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Height { get; set; }
        public string Weight { get; set; }
        public int NumberRange { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
