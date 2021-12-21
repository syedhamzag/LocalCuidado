using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.SalaryAllowance;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryAllowance;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class SalaryAllowanceController : BaseController
    {
        private ISalaryAllowanceService _SalaryAllowance;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public SalaryAllowanceController(ISalaryAllowanceService SalaryAllowance, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _SalaryAllowance = SalaryAllowance;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _SalaryAllowance.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateSalaryAllowance> reports = new List<CreateSalaryAllowance>();
            foreach (GetSalaryAllowance item in entities)
            {
                var report = new CreateSalaryAllowance();
                report.SalaryAllowanceId = item.SalaryAllowanceId;
                report.StartDate = item.StartDate;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateSalaryAllowance();
            model.StaffPersonalInfoId = staffId;
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSalaryAllowance model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            PostSalaryAllowance post = new PostSalaryAllowance();
            post.SalaryAllowanceId = model.SalaryAllowanceId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.AllowanceType = model.AllowanceType;
            post.Amount = model.Amount;
            post.Reoccurent = model.Reoccurent;
            post.Percentage = model.Percentage;
            post.StartDate = model.StartDate;
            post.EndDate = model.EndDate;



            var result = await _SalaryAllowance.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "SalaryAllowance successfully added to staff" : "An Error Occurred"
            });

            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
