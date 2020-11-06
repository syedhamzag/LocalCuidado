using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Extensions;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.Admin.Services.ShiftBooking;
using AwesomeCare.Admin.Models;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.StaffWorkTeam;
using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffWorkTeamController : BaseController
    {


        private ILogger<StaffWorkTeamController> _logger;
        private IStaffWorkTeamService _staffWorkTeamService;

        public StaffWorkTeamController(IStaffWorkTeamService staffWorkTeamService, IFileUpload fileUpload, ILogger<StaffWorkTeamController> logger) : base(fileUpload)
        {

            _logger = logger;
            _staffWorkTeamService = staffWorkTeamService;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await _staffWorkTeamService.Get();
            return View(entities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostStaffWorkTeam model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _staffWorkTeamService.Post(model);
            var content = await result.Content.ReadAsStringAsync();
            string msg = result.StatusCode == System.Net.HttpStatusCode.InternalServerError ? "An Error Occurred" : content;
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New record successfully created" : "An error occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}