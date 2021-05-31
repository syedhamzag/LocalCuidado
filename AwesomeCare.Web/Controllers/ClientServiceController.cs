using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.Services;
using AwesomeCare.Web.ViewModels.ClientService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AwesomeCare.Web.Extensions;
using AwesomeCare.Web.Services.Staff;
using System.Security.Claims;

namespace AwesomeCare.Web.Controllers
{
    public class ClientServiceController : BaseController
    {
        private readonly IClientService clientService;
        private readonly IStaffService staffService;
        private readonly IClientServiceDetailService clientServiceDetail;
       
        private readonly ILogger<ClientServiceController> logger;

        public ClientServiceController(IFileUpload fileUpload,
            IClientServiceDetailService clientServiceDetail,
            IClientService clientService,
            IStaffService staffService,
            ILogger<ClientServiceController> logger) : base(fileUpload)
        {
            this.clientServiceDetail = clientServiceDetail;           
            this.logger = logger;
            this.clientService = clientService;
            this.staffService = staffService;
        }
        public async Task<IActionResult> Index()
        {
            var clientServices = await this.clientServiceDetail.Get();

            return View(clientServices);
        }

        public async Task<IActionResult> Add()
        {
            var model = new PostClientServiceViewModel();
            model.ClientServiceDetailItems.Add(new PostClientServiceDetailItem());

            var clients = await clientService.GetClientDetail();

            model.Clients = clients.Select(c => new SelectListItem(c.FullName, c.ClientId.ToString())).ToList();
            HttpContext.Session.Set<List<SelectListItem>>("clients", model.Clients);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PostClientServiceViewModel model)
        {
            model.Clients = HttpContext.Session.Get<List<SelectListItem>>("clients");
            var staffs = await staffService.GetStaffs();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //  List<PostClientServiceDetailReceipt> receipts = new List<PostClientServiceDetailReceipt>();
            var userId = this.User.SubClaim();
            var currentStaff = staffs.FirstOrDefault(s => s.ApplicationUserId == userId);
            model.StaffPersonalInfoId = currentStaff.StaffPersonalInfoId;
            foreach (var file in model.Receipts)
            {
                var filename = currentStaff?.Fullname + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + file.FileName;

                var filePath = await _fileUpload.UploadFile("clientservice", true, filename, file.OpenReadStream());
                model.ClientServiceDetailReceipts.Add(new PostClientServiceDetailReceipt
                {
                    Attachment = filePath
                });

            }

            var jj = JsonConvert.SerializeObject(model);
            var result = await clientServiceDetail.Post(model);
            var content = await result.Content.ReadAsStringAsync();

            logger.LogInformation($"ClientServiceController: {content}");

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation Successful" : "An error occurred" });

            if (!result.IsSuccessStatusCode)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var clientServiceDetails = await clientServiceDetail.GetById(id);
            return View(clientServiceDetails);
        }
    }
}
