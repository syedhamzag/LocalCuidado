using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Communication;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class CommunicationController : BaseController
    {
        private ICommunicationService _communicationService;
        private IStaffService _staffService;

        public CommunicationController(ICommunicationService communicationService,IStaffService staffService, IFileUpload fileUpload):base(fileUpload)
        {
            _communicationService = communicationService;
            _staffService = staffService;
        }
        public async Task<IActionResult> Index()
        {
            var request = await _communicationService.Get();
            var content = await request.Content.ReadAsStringAsync();
            ViewBag.Content = content;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Compose()
        {
            var model = new ComposeMessage();
            var staffs = await _staffService.GetStaffs();

            model.Staffs = staffs.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(s.Fullname, s.ApplicationUserId)).ToList();
            return View(model);
        }
    }
}