using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Investigation;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Investigation;
using AwesomeCare.Model.Models;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Extensions;
using Microsoft.Extensions.Logging;
using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;

namespace AwesomeCare.Admin.Controllers
{
    public class InvestigationController : BaseController
    {
        private readonly IInvestigationService investigationService;
        private readonly IClientService clientService;
        private readonly IStaffService staffService;
        private readonly ILogger<InvestigationController> logger;

        public InvestigationController(IFileUpload fileUpload,
            IInvestigationService investigationService,
            IClientService clientService,
            IStaffService staffService,
            ILogger<InvestigationController> logger) : base(fileUpload)
        {
            this.investigationService = investigationService;
            this.clientService = clientService;
            this.staffService = staffService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var investigations = await investigationService.Get();

            return View(investigations);
        }

        public async Task<IActionResult> Add()
        {
            var model = new PostInvestigationViewModel();
            var clients = await clientService.GetClientDetail();
            var staffs = await staffService.GetStaffs();
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Clients = clients.Select(c => new SelectListItem(c.FullName, c.ClientId.ToString())).ToList();

            model.Staffs.Insert(0, new SelectListItem("", ""));
            model.Clients.Insert(0, new SelectListItem("", ""));

            HttpContext.Session.Set<List<SelectListItem>>("staffs", model.Staffs);
            HttpContext.Session.Set<List<SelectListItem>>("clients", model.Clients);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PostInvestigationViewModel model)
        {
            model.Staffs = HttpContext.Session.Get<List<SelectListItem>>("staffs");
            model.Clients = HttpContext.Session.Get<List<SelectListItem>>("clients");



            if (!ModelState.IsValid)
            {
                return View(model);
            }

            for (int i = 0; i < model.Files.Count; i++)
            {
                var file = model.Files[i];
                if (file.Length > 0)
                {
                    string staff = model.Staffs.FirstOrDefault(s => s.Value == model.StaffPersonalInfoId.ToString())?.Text;
                    string client = model.Clients.FirstOrDefault(c => c.Value == model.ClientId.ToString())?.Text;
                    string filename = staff + "_" + client + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + i + "_" + file.FileName;

                    var attachment = await _fileUpload.UploadFile("investigations", true, filename, file.OpenReadStream(),file.ContentType);

                    model.InvestigationAttachments.Add(new PostInvestigationAttachment
                    {
                        Attachment = attachment
                    });
                    
                }
            }

            var result = await investigationService.Post(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation Successful" : "An error occurred" });

            if (!result.IsSuccessStatusCode)
            {
                this.logger.LogError(content);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var investigationDetails = await investigationService.Get(id);
            return View(investigationDetails);
        }
    }
}
