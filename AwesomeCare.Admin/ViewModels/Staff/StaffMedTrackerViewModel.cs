using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class StaffMedTrackerViewModel
    {
        public int StaffMedTrackerId { get; set; }
        public DateTime MedTrackDate { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int RotaId { get; set; }

        public int ClientMedId { get; set; }
        public string DoseGiven { get; set; }
        public int Status { get; set; }

        public List<SelectListItem> StaffList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Rotas { get; set; } = new List<SelectListItem>();
    }
}
