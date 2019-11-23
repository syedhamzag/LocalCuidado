﻿using System;
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
using AwesomeCare.Admin.Services.ClientInvolvingPartyBase;
using AwesomeCare.Admin.ViewModels.Admin;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRota;

namespace AwesomeCare.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IBaseRecordService _baseRecordService;
        private readonly IClientInvolvingPartyBase _clientInvolvingPartyBaseService;
       
        public AdminController(IBaseRecordService baseRecordService, IClientInvolvingPartyBase clientInvolvingPartyBaseService)
        {
            _baseRecordService = baseRecordService;
            _clientInvolvingPartyBaseService = clientInvolvingPartyBaseService;
            
        }
        #region BaseRecord
        public async Task<IActionResult> BaseRecord()
        {
            var baseRecordsWithItems = await _baseRecordService.GetBaseRecordsWithItems();
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
            catch (Refit.ApiException ee)
            {

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("BaseRecord");
        }

        public IActionResult AddBaseRecordItem(int? baseRecordId, string baseRecord)
        {
            if (!baseRecordId.HasValue || string.IsNullOrEmpty(baseRecord))
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
                if (baseRecordItem != null)
                    this.SetOperationStatus(new Models.OperationStatus { Message = "Operation Successful", IsSuccessful = true });
            }
            catch (Refit.ApiException ee)
            {
                var message = ee.GetException();
                this.SetOperationStatus(new Models.OperationStatus { Message = message, IsSuccessful = false });
            }
            catch (Exception)
            {
                this.SetOperationStatus(new Models.OperationStatus { Message = "An Error Occurred", IsSuccessful = false });
            }
            return RedirectToAction("BaseRecord");
        }
        #endregion

        #region Client Involving Party Base

        public async Task<IActionResult> InvolvingParty()
        {
            var viewModel = new InvolvingPartyViewModel();
            viewModel.InvolvingPartyItems = await _clientInvolvingPartyBaseService.Get();
            return View("ClientInvolvingPartyBase", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvolvingParty(InvolvingPartyViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Add or Update
                if (model.Id == 0)
                {
                    var newItem = new PostClientInvolvingPartyItem
                    {
                        Description = model.Description,
                        ItemName = model.ItemName
                    };
                    var result = await _clientInvolvingPartyBaseService.Post(newItem);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Involving Party successfully registered" : "An Error Occurred" });

                }
                else
                {
                    var updateItem = new PutClientInvolvingPartyItem
                    {
                        ClientInvolvingPartyItemId = model.Id,
                        Deleted= false,
                        Description = model.Description,
                        ItemName = model.ItemName
                    };
                    var result = await _clientInvolvingPartyBaseService.Put(updateItem);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Involving Party successfully updated" : "An Error Occurred" });

                }
                return RedirectToAction("InvolvingParty");
            }
            //if it gets here something happened

            model.InvolvingPartyItems = await _clientInvolvingPartyBaseService.Get();
            return View("ClientInvolvingPartyBase", model);
        }

        #endregion
    

    }
}