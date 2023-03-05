using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.Lateness;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffLatenessController : BaseController
    {
        private IStaffLatenessService _StaffLateness;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;
        private IClientRotaNameService _clientRotaNameService;

        public StaffLatenessController(IStaffLatenessService StaffLateness, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord, IClientRotaNameService clientRotaNameService) : base(fileUpload)
        {
            _StaffLateness = StaffLateness;
            _staffService = staffService;
            _baseRecord = baseRecord;
            _clientRotaNameService = clientRotaNameService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffLateness.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffLateness> reports = new List<CreateStaffLateness>();
            foreach (GetStaffLateness item in entities)
            {
                var report = new CreateStaffLateness();
                report.StaffLatenessId = item.StaffLatenessId;
                report.StatusName = _baseRecord.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.SN = item.SN;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                report.Date = item.Date;
                report.TimeCritical = item.TimeCritical;
                report.Reason = item.Reason;
                report.Response = item.Response;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateStaffLateness();
            var staff = await _staffService.GetStaffs();
            model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int LatenessId)
        {
            var model = new CreateStaffLateness();
            var Lateness = await _StaffLateness.Get(LatenessId);
            var staff = await _staffService.GetStaffs();
            model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            model.StaffLatenessId = Lateness.StaffLatenessId;
            model.Status = Lateness.Status;
            model.StaffPersonalInfoId = Lateness.StaffPersonalInfoId;
            model.SN = Lateness.SN;
            model.Response = Lateness.Response;
            model.Reason = Lateness.Reason;
            model.Date = Lateness.Date;
            model.Rota = Lateness.Rota;
            model.TimeCritical = Lateness.TimeCritical;
            model.StaffName = staff.Where(s=>s.StaffPersonalInfoId==Lateness.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(model);

        }
        public async Task<IActionResult> View(int LatenessId)
        {
            var model = new CreateStaffLateness();
            var Lateness = await _StaffLateness.Get(LatenessId);
            var staff = await _staffService.GetStaffs();
            model.StaffLatenessId = Lateness.StaffLatenessId;
            model.Status = Lateness.Status;
            model.StaffPersonalInfoId = Lateness.StaffPersonalInfoId;
            model.SN = Lateness.SN;
            model.Response = Lateness.Response;
            model.Reason = Lateness.Reason;
            model.Date = Lateness.Date;
            model.Rota = Lateness.Rota;
            model.TimeCritical = Lateness.TimeCritical;
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == Lateness.StaffPersonalInfoId).FirstOrDefault().Fullname;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffLateness model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffs();
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var rotas = await _clientRotaNameService.Get();
                model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
                return View(model);
            }
            var lateness = await  _StaffLateness.Get();
            var sn = lateness.Count > 0 ? lateness.Count : 0;
            PostStaffLateness post = new PostStaffLateness();
            post.StaffLatenessId = model.StaffLatenessId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Status = model.Status;
            post.SN = (sn+1);
            post.Response = model.Response;
            post.Reason = model.Reason;
            post.Date = model.Date;
            post.Rota = model.Rota;
            post.TimeCritical = model.TimeCritical;



            var result = await _StaffLateness.Post(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "StaffLateness successfully added to staff" : "An Error Occurred"
            });

            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffLateness model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffs();
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var rotas = await _clientRotaNameService.Get();
                model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
                return View(model);
            }
            PutStaffLateness post = new PutStaffLateness();
            post.StaffLatenessId = model.StaffLatenessId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Status = model.Status;
            post.SN = model.SN;
            post.Response = model.Response;
            post.Reason = model.Reason;
            post.Date = model.Date;
            post.Rota = model.Rota;
            post.TimeCritical = model.TimeCritical;



            var result = await _StaffLateness.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "StaffLateness successfully updated to staff" : "An Error Occurred"
            });

            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
