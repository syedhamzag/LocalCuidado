using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp
{
    public class PostClientBodyTemp
    {
        public PostClientBodyTemp()
        {
            OfficerToAct = new List<PostBodyTempOfficerToAct>();
            Physician = new List<PostBodyTempPhysician>();
            StaffName = new List<PostBodyTempStaffName>();
        }

        public List<PostBodyTempOfficerToAct> OfficerToAct { get; set; }
        public List<PostBodyTempPhysician> Physician { get; set; }
        public List<PostBodyTempStaffName> StaffName { get; set; }

        public int BodyTempId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetTemp { get; set; }
        public string TargetTempAttach { get; set; }
        public string CurrentReading { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
