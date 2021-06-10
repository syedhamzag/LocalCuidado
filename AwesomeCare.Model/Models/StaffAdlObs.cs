﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffAdlObs
    {
    public int ObservationID {get; set;}
    public int StaffId {get; set;}
    public DateTime Date {get; set;}
    public DateTime NextCheckDate {get; set;}
    public string Details {get; set;}
    public int ClientId {get; set;}
    public int UnderstandingofEquipment {get; set;}
    public int UnderstandingofService {get; set;}
    public int UnderstandingofControl {get; set;}
    public int FivePrinciples {get; set;}
    public string Comments {get; set;}
    public string ActionRequired {get; set;}
    public int OfficerToAct {get; set;}
    public DateTime Deadline {get; set;}
    public int Status {get; set;}
    public string Remarks {get; set;}
    public string URL {get; set;}
    public string Attachment {get; set;}

    public virtual Client Client { get; set; }
    public virtual StaffPersonalInfo Staff { get; set; }
    }

}
