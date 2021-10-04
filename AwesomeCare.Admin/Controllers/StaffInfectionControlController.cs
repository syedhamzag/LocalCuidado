using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffInfectionControlController : BaseController
    {
        private IStaffInfectionControlService _infectionService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public StaffInfectionControlController(IStaffInfectionControlService infectionService, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseService) : base(fileUpload)
        {
            _infectionService = infectionService;
            _staffService = staffService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _infectionService.Get();

            var staffs = await _staffService.GetStaffs();
            List<CreateStaffInfectionControl> reports = new List<CreateStaffInfectionControl>();
            foreach (GetStaffInfectionControl item in entities)
            {
                var report = new CreateStaffInfectionControl();
                report.StaffPersonalInfoId = item.StaffPersonalInfoId;
                report.InfectionId = item.InfectionId;
                report.VaccName = _baseService.GetBaseRecordItemById(item.VaccStatus).Result.ValueName;
                report.StaffName = staffs.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname).FirstOrDefault();
                report.InfectionName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateStaffInfectionControl();
            model.StaffPersonalInfoId = staffId;
            var client = await _staffService.GetStaffs();
            model.StaffName = client.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);
        }

        public async Task<IActionResult> View(int InfectionControlId)
        {
            var putEntity = await GetInfectionControl(InfectionControlId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int InfectionControlId)
        {
            var putEntity = await GetInfectionControl(InfectionControlId);
            return View(putEntity);
        }

        public async Task<CreateStaffInfectionControl> GetInfectionControl(int InfectionControlId)
        {
            var infection = await _infectionService.Get(InfectionControlId);
            var putEntity = new CreateStaffInfectionControl
            {
                StaffPersonalInfoId = infection.StaffPersonalInfoId,
                Guideline = infection.Guideline,
                InfectionId = infection.InfectionId,
                Remarks = infection.Remarks,
                Status = infection.Status,
                TestDate = infection.TestDate,
                Type = infection.Type,
                VaccStatus = infection.VaccStatus,
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffInfectionControl model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffName = staffs.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).Select(s => s.Fullname).FirstOrDefault();
                return View(model);
            }

            PostStaffInfectionControl infection = new PostStaffInfectionControl();

            infection.StaffPersonalInfoId = model.StaffPersonalInfoId;
            infection.Guideline = model.Guideline;
            infection.InfectionId = model.InfectionId;
            infection.Remarks = model.Remarks;
            infection.Status = model.Status;
            infection.TestDate = model.TestDate;
            infection.Type = model.Type;
            infection.VaccStatus = model.VaccStatus;

            var json = JsonConvert.SerializeObject(infection);
            var result = await _infectionService.Create(infection);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Infection Control successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffInfectionControl model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffName = staffs.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).Select(s => s.Fullname).FirstOrDefault();
                return View(model);
            }

            PutStaffInfectionControl put = new PutStaffInfectionControl();

            put.StaffPersonalInfoId = model.StaffPersonalInfoId;
            put.Guideline = model.Guideline;
            put.InfectionId = model.InfectionId;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.TestDate = model.TestDate;
            put.Type = model.Type;
            put.VaccStatus = model.VaccStatus;

            var entity = await _infectionService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
