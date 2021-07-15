using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.ViewModels.PersonalDetail;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class PersonalDetailController : BaseController
    {
        private IStaffService _staffService;

        public PersonalDetailController (IFileUpload fileUpload, IStaffService staffService) : base(fileUpload)
        {
            _staffService = staffService;
        }

        public async Task<IActionResult> Index()
        {
            var staffs = await _staffService.GetStaffs();

            var model = new CreatePersonalDetail();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            
            
            
            return View(model);
        }
    }
}
