﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientPainChart
    {
        public ClientPainChart()
        {
            Physician = new HashSet<PainChartPhysician>();
            StaffName = new HashSet<PainChartStaffName>();
            OfficerToAct = new HashSet<PainChartOfficerToAct>();
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
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<PainChartOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<PainChartStaffName> StaffName { get; set; }
        public virtual ICollection<PainChartPhysician> Physician { get; set; }
    }

}
