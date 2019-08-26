using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class AdminController : Controller
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
            var updateItem = Mapper.Map<PutBaseRecordItem>(item);
            var baseRecordItem = await _baseRecordService.UpdateBaseRecordItem(updateItem);
            return RedirectToAction("BaseRecord");
        }
    }
}