using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp
{
    public class PutClientBodyTemp
    {
        public PutClientBodyTemp()
        {
            OfficerToAct = new List<PutBodyTempOfficerToAct>();
            Physician = new List<PutBodyTempPhysician>();
            StaffName = new List<PutBodyTempStaffName>();
        }

        public List<PutBodyTempOfficerToAct> OfficerToAct { get; set; }
        public List<PutBodyTempPhysician> Physician { get; set; }
        public List<PutBodyTempStaffName> StaffName { get; set; }

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
