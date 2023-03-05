using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.IncidentReport;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.IncidentReport;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Extensions;
using AwesomeCare.Admin.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AwesomeCare.Admin.Controllers
{
    public class IncidentReportingController : BaseController
    {
        private readonly IncidentReportService incidentReportService;
        private readonly IStaffService staffService;
        private readonly IClientService clientService;
        private readonly ILogger<IncidentReportingController> logger;

        public IncidentReportingController(IFileUpload fileUpload,
            IncidentReportService incidentReportService,
            IStaffService staffService,
            IClientService clientService,
            ILogger<IncidentReportingController> logger) : base(fileUpload)
        {
            this.incidentReportService = incidentReportService;
            this.staffService = staffService;
            this.clientService = clientService;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            var incidents = await incidentReportService.GetStaffReports();
            return View(incidents);
        }

        [HttpGet]
        public async Task<IActionResult> AddReport()
        {
            var model = new PostStaffIncidentReportViewModel();
            var staffs = await staffService.GetStaffs();
            var clients = await clientService.GetClientDetail();

         
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Clients = clients.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();

            model.Staffs.Insert(0, new SelectListItem("", ""));
            model.Clients.Insert(0, new SelectListItem("", ""));

            HttpContext.Session.Set<List<SelectListItem>>("staffs", model.Staffs);
            HttpContext.Session.Set<List<SelectListItem>>("clients", model.Clients);




            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReport(PostStaffIncidentReportViewModel model)
        {
            model.Staffs = HttpContext.Session.Get<List<SelectListItem>>("staffs");
            model.Clients = HttpContext.Session.Get<List<SelectListItem>>("clients");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.UploadAttachment?.Length > 0)
            {

                var filename = model.Staffs.FirstOrDefault(s => s.Value == model.StaffInvolvedId.ToString())?.Text + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + model.UploadAttachment.FileName;
                string filepath = await this._fileUpload.UploadFile("incidentreport", true, filename, model.UploadAttachment.OpenReadStream(), model.UploadAttachment.ContentType);

                model.Attachment = filepath;
            }
            var result = await incidentReportService.PostStaffReport(model);
            var content = await result.Content.ReadAsStringAsync();


            SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Operation Successful" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError($"IncidentReport AddReport: {content}");
                return View(model);
            }

            return RedirectToAction("Reports");
        }


        [HttpGet]
        public async Task<IActionResult> ReportDetails(int id)
        {
            var incident = await incidentReportService.GetStaffReportById(id);
            return View(incident);
        }

    }
}
