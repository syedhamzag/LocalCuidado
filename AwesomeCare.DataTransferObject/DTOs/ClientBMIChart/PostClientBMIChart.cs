using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBMIChart
{
    public class PostClientBMIChart
    {
        public PostClientBMIChart()
        {
            OfficerToAct = new List<PostBMIChartOfficerToAct>();
            Physician = new List<PostBMIChartPhysician>();
            StaffName = new List<PostBMIChartStaffName>();
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

        public List<PostBMIChartOfficerToAct> OfficerToAct { get; set; }
        public List<PostBMIChartPhysician> Physician { get; set; }
        public List<PostBMIChartStaffName> StaffName { get; set; }
    }
}
