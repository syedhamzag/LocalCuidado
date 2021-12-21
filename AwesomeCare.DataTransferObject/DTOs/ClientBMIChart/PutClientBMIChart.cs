using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class PutClientBMIChart
    {
        public PutClientBMIChart()
        {
            OfficerToAct = new List<PutBMIChartOfficerToAct>();
            Physician = new List<PutBMIChartPhysician>();
            StaffName = new List<PutBMIChartStaffName>();
        }

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

        public List<PutBMIChartOfficerToAct> OfficerToAct { get; set; }
        public List<PutBMIChartPhysician> Physician { get; set; }
        public List<PutBMIChartStaffName> StaffName { get; set; }
    }
}
