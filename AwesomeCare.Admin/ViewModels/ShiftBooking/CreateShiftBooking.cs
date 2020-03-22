using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ShiftBooking
{
    public class CreateShiftBooking : PostShiftBooking
    {
        public CreateShiftBooking()
        {
            YesNo = new List<SelectListItem>
            {
                new SelectListItem("No","No"),
                new SelectListItem("Yes","Yes")
            };

            Staffs = new List<SelectListItem>();
            WorkTeams = new List<SelectListItem>();
            Rotas = new List<SelectListItem>();
        }

        public List<SelectListItem> YesNo { get; set; }
        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> WorkTeams { get; set; }
        public List<SelectListItem> Rotas { get; set; }
        public string RequiresDriver { get; set; }
    }
}
