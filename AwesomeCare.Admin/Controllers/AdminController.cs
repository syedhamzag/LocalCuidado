using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Admin.Extensions;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IBaseRecordService _baseRecordService;
        public AdminController(IBaseRecordService baseRecordService)
        {
            _baseRecordService = baseRecordService;
        }
        public async Task<IActionResult> BaseRecord()
        {
            var baseRecordsWithItems =await _baseRecordService.GetBaseRecordsWithItems();
            return View(baseRecordsWithItems);
        }

        public async Task<IActionResult> EditBaseRecordItem(int baseRecordItemId)
        {
            var baseRecordItem = await _baseRecordService.GetBaseRecordItemById(baseRecordItemId);
            return View(baseRecordItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBaseRecordItem(GetBaseRecordItem item)
        {
            try
            {
                var updateItem = Mapper.Map<PutBaseRecordItem>(item);
                var baseRecordItem = await _baseRecordService.UpdateBaseRecordItem(updateItem);
            }
            catch(Refit.ApiException ee)
            {

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("BaseRecord");
        }

        public IActionResult AddBaseRecordItem(int? baseRecordId,string baseRecord)
        {
            if(!baseRecordId.HasValue || string.IsNullOrEmpty(baseRecord))
            {
                return NotFound();
            }
            var postbaseRecordItem = new PostBaseRecordItem();
            postbaseRecordItem.BaseRecordId = baseRecordId.Value;
            ViewBag.BaseRecord = baseRecord;
            return View(postbaseRecordItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBaseRecordItem(PostBaseRecordItem model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var baseRecordItem = await _baseRecordService.PostBaseRecordItem(model);
                if(baseRecordItem !=null)
                    this.SetOperationStatus(new Models.OperationStatus { Message = "Operation Successful", IsSuccessful = true });
            }
            catch(Refit.ApiException ee)
            {
                var message = ee.GetException();
              this.SetOperationStatus( new Models.OperationStatus { Message = message, IsSuccessful = false });
            }
            catch (Exception )
            {
                this.SetOperationStatus(new Models.OperationStatus { Message = "An Error Occurred", IsSuccessful = false });
            }
            return RedirectToAction("BaseRecord");
        }
    }
}