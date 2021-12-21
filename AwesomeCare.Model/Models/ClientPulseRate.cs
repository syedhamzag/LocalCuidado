﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientPulseRate
    {
        public ClientPulseRate()
        {
            Physician = new HashSet<PulseRatePhysician>();
            StaffName = new HashSet<PulseRateStaffName>();
            OfficerToAct = new HashSet<PulseRateOfficerToAct>();
        }
        public int PulseRateId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetPulse { get; set; }
        public string TargetPulseAttach { get; set; }
        public string CurrentPulse { get; set; }
        public string Chart { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<PulseRateOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<PulseRateStaffName> StaffName { get; set; }
        public virtual ICollection<PulseRatePhysician> Physician { get; set; }
    }

}
