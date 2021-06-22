using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientSeizure
{
    public class GetClientSeizure
    {
        public GetClientSeizure()
        {
            OfficerToAct = new List<GetSeizureOfficerToAct>();
            Physician = new List<GetSeizurePhysician>();
            StaffName = new List<GetSeizureStaffName>();
        }

        public List<GetSeizureOfficerToAct> OfficerToAct { get; set; }
        public List<GetSeizurePhysician> Physician { get; set; }
        public List<GetSeizureStaffName> StaffName { get; set; }

        public int SeizureId { get; set; }
        public int ClientId { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int SeizureType { get; set; }
        public string SeizureTypeAttach { get; set; }
        public int SeizureLength { get; set; }
        public string SeizureLengthAttach { get; set; }
        public int Often { get; set; }
        public string WhatHappened { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
