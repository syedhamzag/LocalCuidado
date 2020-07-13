using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Web.Services.IncidentReport;
using AwesomeCare.Web.Services.Staff;
using AwesomeCare.Web.ViewModels.IncidentReport;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Web.Extensions;
using AwesomeCare.Web.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AwesomeCare.Web.Controllers
{
    public class IncidentReportingController : BaseController
    {
        private readonly IIncidentReportService incidentReportService;
        private readonly IStaffService staffService;
        private readonly IClientService clientService;
        private readonly ILogger<IncidentReportingController> logger;
        private readonly IFileUpload fileUpload;

        public IncidentReportingController(IFileUpload fileUpload,
            IIncidentReportService incidentReportService,
            IStaffService staffService,
            IClientService clientService,
            ILogger<IncidentReportingController> logger) 
        {
            this.incidentReportService = incidentReportService;
            this.staffService = staffService;
            this.clientService = clientService;
            this.logger = logger;
            this.fileUpload = fileUpload;
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


            var staffPersonalInfoId = this.User.StaffPersonalInfoId();

            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString(), s.StaffPersonalInfoId.ToString() == staffPersonalInfoId)).ToList();
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

            model.ReportingStaffId = int.Parse(this.User.StaffPersonalInfoId());

            if (model.UploadAttachment?.Length > 0)
            {

                var filename = model.Staffs.FirstOrDefault(s => s.Value == model.ReportingStaffId.ToString())?.Text + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + model.UploadAttachment.FileName;
                string filepath = await this.fileUpload.UploadFile("incidentreport", true, filename, model.UploadAttachment.OpenReadStream());

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
