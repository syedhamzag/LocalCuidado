﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.Medication;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.ViewModels.Medication;

namespace AwesomeCare.Admin.Controllers
{
    public class MedicationController : BaseController
    {
        private IMedicationService _medicationService;
        private IStaffService _staffService;
        private IClientService _clientService;
        private IClientRotaNameService _clientRotaNameService;

        public MedicationController(IMedicationService medicationService, IFileUpload fileUpload, IStaffService staffService, IClientRotaNameService clientRotaNameService, IClientService clientService) : base(fileUpload)
        {
            _medicationService = medicationService;
            _staffService = staffService;
            _clientRotaNameService = clientRotaNameService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _medicationService.Get();
            return View(entities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostMedication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.Post(model);

            var content = await entity.Content.ReadAsStringAsync();


            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful =entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : content
            });
            if (entity != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _medicationService.Get(id);
            if (entity == null) return NotFound();

            var putEntity = new PutMedication
            {
                Deleted = entity.Deleted,
                MedicationId = entity.MedicationId,
                MedicationName = entity.MedicationName,
                Strength = entity.Strength
            };
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PutMedication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.Put(model);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity != null,
                Message = entity != null ? "Successful" : "Operation failed"
            });
            if (entity != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public  async Task<IActionResult> StaffMedTracker(int clientmedId)
        {
            var model = new StaffMedTrackerViewModel();
            model.ClientMedId = clientmedId;
            var staffs = await _staffService.GetStaffs();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StaffMedTracker(StaffMedTrackerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                var rotas = await _clientRotaNameService.Get();
                model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

                return View(model);
            }
            //var post = new PostStaffMedTracker
            //{
            //    ClientMedId = model.ClientMedId,
            //    DoseGiven = model.DoseGiven,
            //    MedTrackDate = model.MedTrackDate,
            //    RotaId = model.RotaId,
            //    StaffMedTrackerId = model.StaffMedTrackerId,    
            //    StaffPersonalInfoId = model.StaffPersonalInfoId,
            //    Status = model.Status
            //};
            //var entity = await _medicationService.Create(post);

            //SetOperationStatus(new Models.OperationStatus
            //{
            //    IsSuccessful = entity.StaffMedTrackerId > 0,
            //    Message = entity.StaffMedTrackerId > 0 ? "Successful" : "Error"
            //});
            //if (entity != null)
            //{
            //    return RedirectToAction("MedTracker");
            //}
            return View(model);

        }
        public IActionResult MedTracker()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MedTracker(string startDate, string stopDate)
        {
            //var liveRotaViewModel = new LiveMed();
            var sdate = string.IsNullOrWhiteSpace(startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : startDate;
            var edate = string.IsNullOrWhiteSpace(stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : stopDate;
            var medTracker = await _medicationService.MedTracker(sdate, edate);

            return View(medTracker);
        }
        public async Task<IActionResult> MARChart()
        {
            var model = new MedTrackerViewModel();
            var clients = await _clientService.GetClientDetail();
            model.ClientList = clients.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MARChart(MedTrackerViewModel model)
        {
            int year = Convert.ToInt32(model.StartDate.ToString("yyyy"));
            int month = Convert.ToInt32(model.StartDate.ToString("MM"));
            int lastday = DateTime.DaysInMonth(year, month);
            model.FirstDay = new DateTime(year, month, 01);
            model.LastDay = new DateTime(year, month, lastday);
            model.DaysInMonth = lastday;
            var clients = await _clientService.GetClientDetail();
            model.ClientList = clients.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            var sdate = model.FirstDay.ToString("yyyy-MM-dd");
            var edate = model.LastDay.ToString("yyyy-MM-dd");

            var medTracker = await _medicationService.MedTracker(sdate, edate);
            model.MedTracker = medTracker.Where(s=>s.ClientId==model.ClientId).ToList();
            return View(model);
        }
    }
}