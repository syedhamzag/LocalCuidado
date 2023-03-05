using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.Medication
{
    public class MedTrackerViewModel
    {
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysInMonth { get; set; }
        public string SelectedMonth { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<MedTracker> MedTracker { get; set; }
    }
}
