using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Equipment;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.Admin.ViewModels.PersonalDetail;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class EquipmentController : BaseController
    {
        private IEquipmentService _EquipmentService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public EquipmentController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IEquipmentService EquipmentService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _EquipmentService = EquipmentService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateEquipment();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateEquipment model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostEquipment postlog = new PostEquipment();
            if (model.Attach != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "Attachment", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = "No Image";
            }
            postlog.EquipmentId = model.EquipmentId;
            postlog.PersonalDetailId = model.ClientId;
            postlog.Location = model.Location;
            postlog.Name = model.Name;
            postlog.Type = model.Type;
            postlog.NextServiceDate = model.NextServiceDate;
            postlog.ServiceDate = model.ServiceDate;
            postlog.Status = model.Status;
            postlog.PersonToAct = model.PersonToAct;
            postlog.Attachment = model.Attachment;

            var json = JsonConvert.SerializeObject(postlog);
            var result = await _EquipmentService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Equipment successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
