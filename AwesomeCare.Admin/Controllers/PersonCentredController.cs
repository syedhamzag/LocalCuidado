using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.PersonCentred;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.Admin.ViewModels.PersonalDetail;
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class PersonCentredController : BaseController
    {
        private IPersonCentredService _PersonCentredService;
        private IStaffService _staffService;
        private IClientService _clientService;
        private IBaseRecordService _baseRecord;

        public PersonCentredController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IPersonCentredService PersonCentredService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _PersonCentredService = PersonCentredService;
            _baseRecord = baseRecord;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreatePersonCentred();
            var bases = await _baseRecord.GetBaseRecord();
            var id = bases.Where(s => s.KeyName == "Focus").FirstOrDefault().BaseRecordId;
            var items = await _baseRecord.GetBaseRecordWithItems(id);
            model.FocusList = items.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePersonCentred model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostPersonCentred postlog = new PostPersonCentred();

            postlog.ClientId = model.ClientId;
            postlog.PersonCentredId = model.PersonCentredId;
            postlog.Class = model.Class;
            postlog.ExpSupport = model.ExpSupport;
            postlog.Focus = model.Focus.Select(o => new PostPersonCentredFocus{ BaseRecordId = o, PersonCentredId = model.PersonCentredId }).ToList();

            var result = await _PersonCentredService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New PersonCentred successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
