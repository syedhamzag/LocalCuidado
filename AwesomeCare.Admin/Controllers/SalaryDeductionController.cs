using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.SalaryDeduction;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryDeduction;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class SalaryDeductionController : BaseController
    {
        private ISalaryDeductionService _SalaryDeduction;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public SalaryDeductionController(ISalaryDeductionService SalaryDeduction, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _SalaryDeduction = SalaryDeduction;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _SalaryDeduction.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateSalaryDeduction> reports = new List<CreateSalaryDeduction>();
            foreach (GetSalaryDeduction item in entities)
            {
                var report = new CreateSalaryDeduction();
                report.SalaryDeductionId = item.SalaryDeductionId;
                report.StartDate = item.StartDate;
                report.EndDate = item.EndDate;
                report.Percentage = item.Percentage;
                report.Amount = item.Amount;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateSalaryDeduction();
            model.StaffPersonalInfoId = staffId;
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        public async Task<IActionResult> Edit(int salaryId)
        {
            var item = await _SalaryDeduction.Get(salaryId);
            var staff = await _staffService.GetStaffs();
            var report = new CreateSalaryDeduction();
            report.SalaryDeductionId = item.SalaryDeductionId;
            report.StartDate = item.StartDate;
            report.EndDate = item.EndDate;
            report.Percentage = item.Percentage;
            report.Amount = item.Amount;
            report.StaffPersonalInfoId = item.StaffPersonalInfoId;
            report.Reoccurent = item.Reoccurent;
            report.AllowanceType = item.AllowanceType;
            report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(report);

        }
        public async Task<IActionResult> View(int salaryId)
        {
            var item = await _SalaryDeduction.Get(salaryId);
            var staff = await _staffService.GetStaffs();
            var report = new CreateSalaryDeduction();
            report.SalaryDeductionId = item.SalaryDeductionId;
            report.StartDate = item.StartDate;
            report.EndDate = item.EndDate;
            report.Percentage = item.Percentage;
            report.Amount = item.Amount;
            report.StaffPersonalInfoId = item.StaffPersonalInfoId;
            report.Reoccurent = item.Reoccurent;
            report.AllowanceType = item.AllowanceType;
            report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(report);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSalaryDeduction model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            PostSalaryDeduction post = new PostSalaryDeduction();
            post.SalaryDeductionId = model.SalaryDeductionId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.AllowanceType = model.AllowanceType;
            post.Amount = model.Amount;
            post.Reoccurent = model.Reoccurent;
            post.Percentage = model.Percentage;
            post.StartDate = model.StartDate;
            post.EndDate = model.EndDate;



            var result = await _SalaryDeduction.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "SalaryDeduction successfully added to staff" : "An Error Occurred"
            });

            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
