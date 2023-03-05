using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffLateness
    {
        public int StaffLatenessId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int SN { get; set; }
        public DateTime Date { get; set; }
        public int Rota { get; set; }
        public DateTime TimeCritical { get; set; }
        public string Reason { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }
        public string StaffName { get; set; }
        public string StatusName { get; set; }
        public List<SelectListItem> Rotas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StaffList { get; set; }
    }
}
