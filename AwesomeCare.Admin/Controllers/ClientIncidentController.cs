using AutoMapper;
using AwesomeCare.Admin.Extensions;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.IncidentReport;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.IncidentReport;
using AwesomeCare.DataTransferObject.DTOs.IncidentReporting;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientIncidentController : BaseController
    {
        private readonly IncidentReportService incidentReportService;
        private readonly IStaffService staffService;
        private readonly IClientService clientService;
        private readonly ILogger<IncidentReportingController> logger;

        public ClientIncidentController(IFileUpload fileUpload,
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
            var incidents = await incidentReportService.Get();
            return View(incidents);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int clientId)
        {
            var model = new PostIncidentReportViewModel();
            var staffs = await staffService.GetStaffs();

            model.ClientId = clientId;
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Staffs.Insert(0, new SelectListItem("", ""));
            HttpContext.Session.Set<List<SelectListItem>>("staffs", model.Staffs);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PostIncidentReportViewModel model)
        {
            model.Staffs = HttpContext.Session.Get<List<SelectListItem>>("staffs");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.UploadAttachment?.Length > 0)
            {

                var filename = model.Staffs.FirstOrDefault(s => s.Value == model.StaffInvolvedId.ToString())?.Text + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + model.UploadAttachment.FileName;
                string filepath = await this._fileUpload.UploadFile("incidentreport", true, filename, model.UploadAttachment.OpenReadStream(),model.UploadAttachment.ContentType);

                model.Attachment = filepath;
            }
            else
            {
                model.Attachment = "No Image";
            }
            var post = Mapper.Map<PostIncidentReport>(model);
            var result = await incidentReportService.Post(post);
            var content = await result.Content.ReadAsStringAsync();


            SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Operation Successful" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError($"Incident Report Index: {content}");
                return View(model);
            }

            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }
    }
}
