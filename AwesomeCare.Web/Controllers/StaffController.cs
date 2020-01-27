using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Web.Services.Staff;
using AwesomeCare.Web.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeCare.Web.Controllers
{
    public class StaffController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<StaffController> _logger;

        public StaffController(IStaffService staffService, ILogger<StaffController> logger)
        {
            _staffService = staffService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            var model = new CreateStaff();
            model.Education = new List<CreateStaffEducation>();
            model.Education.Add(new CreateStaffEducation());

            model.Trainings = new List<CreateStaffTraining>();
            model.Trainings.Add(new CreateStaffTraining());

            model.References = new List<CreateStaffReference>();
            model.References.Add(new CreateStaffReference());
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(CreateStaff model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ensure to re-upload required files");
                return View(model);
            }

            var staffinfo = Mapper.Map<PostStaffFullInfo>(model);
           

            var result = await _staffService.PostStaffFullInfo(staffinfo);

            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "Your registration was successful" });
                return RedirectToAction("Index", "Home");
            }
            else if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = content });
            }
            else
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "An error occurred" });
            }
            return View(model);
        }
    }
}