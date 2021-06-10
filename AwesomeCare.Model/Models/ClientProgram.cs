﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientProgram
    {
        public int ProgramId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public int ProgramOfChoice { get; set; }
        public int DaysOfChoice { get; set; }
        public int PlaceLocationProgram { get; set; }
        public int DetailsOfProgram { get; set; }
        public string Observation { get; set; }
        public string ActionRequired { get; set; }
        public int OfficerToAct { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo Staff { get; set; }
    }
}
