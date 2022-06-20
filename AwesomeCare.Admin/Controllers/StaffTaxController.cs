using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffTaxController : BaseController
    {
        private IStaffTaxService _StaffTax;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffTaxController(IStaffTaxService StaffTax, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _StaffTax = StaffTax;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffTax.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffTax> reports = new List<CreateStaffTax>();
            foreach (GetStaffTax item in entities)
            {
                var report = new CreateStaffTax();
                report.StaffTaxId = item.StaffTaxId;
                report.Tax = item.Tax;
                report.NI = item.NI;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateStaffTax();
            var staff = await _staffService.GetStaffs();
            model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int taxId)
        {
            var model = new CreateStaffTax();
            var tax = await _StaffTax.Get(taxId);
            var staff = await _staffService.GetStaffs();
            model.StaffTaxId = tax.StaffTaxId;
            model.Tax = tax.Tax;
            model.StaffPersonalInfoId = tax.StaffPersonalInfoId;
            model.NI = tax.NI;
            model.Remarks = tax.Remarks;
            model.StaffName = staff.Where(s=>s.StaffPersonalInfoId==tax.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(model);

        }
        public async Task<IActionResult> View(int taxId)
        {
            var model = new CreateStaffTax();
            var tax = await _StaffTax.Get(taxId);
            var staff = await _staffService.GetStaffs();
            model.StaffTaxId = tax.StaffTaxId;
            model.Tax = tax.Tax;
            model.StaffPersonalInfoId = tax.StaffPersonalInfoId;
            model.NI = tax.NI;
            model.Remarks = tax.Remarks;
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == tax.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffTax model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            PostStaffTax post = new PostStaffTax();
            post.StaffTaxId = model.StaffTaxId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Tax = model.Tax;
            post.NI = model.NI;
            post.Remarks = model.Remarks;



            var result = await _StaffTax.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "StaffTax successfully added to staff" : "An Error Occurred"
            });

            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }


    }
}
