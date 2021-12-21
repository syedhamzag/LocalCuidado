using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Capacity;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.Admin.ViewModels.PersonalDetail;
using AwesomeCare.Admin.Services.Admin;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class CapacityController : BaseController
    {
        private ICapacityService _CapacityService;
        private IStaffService _staffService;
        private IClientService _clientService;
        private IBaseRecordService _baseRecord;

        public CapacityController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, ICapacityService CapacityService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _CapacityService = CapacityService;
            _baseRecord = baseRecord;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateCapacity();
            var bases = await _baseRecord.GetBaseRecord();
            var id = bases.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
            var items = await _baseRecord.GetBaseRecordWithItems(id);      
            model.IndicatorList = items.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCapacity model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostCapacity postlog = new PostCapacity();

            postlog.PersonalDetailId = model.ClientId;
            postlog.CapacityId = model.CapacityId;
            postlog.Implications = model.Implications;
            postlog.Pointer = model.Pointer;
            postlog.Indicator = model.Indicator.Select(o => new PostCapacityIndicator { BaseRecordId = o, CapacityId = model.CapacityId }).ToList();

            var json = JsonConvert.SerializeObject(postlog); 
            var result = await _CapacityService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Capacity successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }

        //public async Task<IActionResult> Edit(int CapacityId)
        //{
        //    var Capacity = _CapacityService.Get(CapacityId);
        //    var staffs = await _staffService.GetStaffs();

        //    var putEntity = new CreateCapacity
        //    {
        //        CapacityId = Capacity.Result.CapacityId,
        //        Pointer = Capacity.Result.Pointer,
        //        ClientId = Capacity.Result.ClientId,
        //        Implications = Capacity.Result.Implications,
        //        Indicator = Capacity.Result.IndicatorList.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
        //        IndicatorList = items.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
        //};
        //    return View(putEntity);
        //}
    }
}
