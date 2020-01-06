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
using AwesomeCare.Admin.Services.ClientInvolvingPartyBase;
using AwesomeCare.Admin.ViewModels.Admin;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;

namespace AwesomeCare.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IBaseRecordService _baseRecordService;
        private readonly IClientInvolvingPartyBase _clientInvolvingPartyBaseService;
        private readonly IClientCareDetails _clientCareDetails;
        public AdminController(IBaseRecordService baseRecordService, IClientInvolvingPartyBase clientInvolvingPartyBaseService, IClientCareDetails clientCareDetails)
        {
            _baseRecordService = baseRecordService;
            _clientInvolvingPartyBaseService = clientInvolvingPartyBaseService;
            _clientCareDetails = clientCareDetails;
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
                        Deleted = false,
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

        #region CareDetails
        public async Task<IActionResult> ClientCareDetailsHeading(int? headingId)
        {
            var model = new ClientCareDetailsHeading();
            if (headingId.HasValue)
            {
                var careDetailsHeading = await _clientCareDetails.Get(headingId.Value);
                model.Delete = careDetailsHeading.Deleted;
                model.ClientCareDetailsHeadingId = careDetailsHeading.ClientCareDetailsHeadingId;
                model.Heading = careDetailsHeading.Heading;
                model.ActionType = ActionType.Put;
            }
            else
            {
                model.Headings = await _clientCareDetails.GetAll();
                model.ActionType = ActionType.Post;
            }


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientCareDetailsHeading(ClientCareDetailsHeading model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.ClientCareDetailsHeadingId > 0 && model.ActionType == ActionType.Put)
            {
                var updateEntity = new PutClientCareDetailsHeading()
                {
                    Deleted = model.Delete,
                    Heading = model.Heading,
                    ClientCareDetailsHeadingId = model.ClientCareDetailsHeadingId
                };
                var result = await _clientCareDetails.Put(updateEntity);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "CareDetails successfully updated" : "An Error Occurred" });

            }
            else
            {
                var newEntity = new PostClientCareDetailsHeading()
                {
                    Deleted = false,
                    Heading = model.Heading
                };
                var result = await _clientCareDetails.Post(newEntity);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New CareDetails successfully registered" : "An Error Occurred" });

            }

            return RedirectToAction("ClientCareDetailsHeading");
        }

        public async Task<IActionResult> ClientCareDetailsTasks(int? headingId,int? taskId)
        {
            if (!headingId.HasValue)
            {
                return NotFound();
            }

            var model = new ClientCareDetailsTask();
            var result = await _clientCareDetails.GetHeadingWithTasks(headingId.Value);
            if (taskId.HasValue)
            {
                model.SelectedTaskId = taskId.Value;
                model.ActionType = ActionType.Put;
                model.Task = result.Tasks.FirstOrDefault(c => c.ClientCareDetailsTaskId == taskId.Value)?.Task;
                model.HeaderId = result.ClientCareDetailsHeadingId;
                model.Heading = result.Heading;
            }
            else
            {
                model.HeaderId = result.ClientCareDetailsHeadingId;
                model.Tasks = result.Tasks;
                model.Heading = result.Heading;
                model.ActionType = ActionType.Post;
            }
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientCareDetailsTasks(ClientCareDetailsTask model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.ActionType == ActionType.Put && model.SelectedTaskId > 0)
            {
                var putEntity = new PutClientCareDetailsTask
                {
                    Task = model.Task,
                    ClientCareDetailsHeadingId = model.HeaderId,
                    ClientCareDetailsTaskId = model.SelectedTaskId,
                    Deleted = model.Delete
                };
                var result = await _clientCareDetails.PutClientDetailsTask(putEntity);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "CareDetails Task successfully updated" : "An Error Occurred" });

            }
            else
            {
                var newEntity = new PostClientCareDetailsTask()
                {
                    ClientCareDetailsHeadingId = model.HeaderId,
                    Task = model.Task
                };
                var result = await _clientCareDetails.PostClientDetailsTask(newEntity);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New CareDetails Task successfully registered" : "An Error Occurred" });

            }

            return RedirectToAction("ClientCareDetailsTasks", new { headingId = model.HeaderId });
        }
        #endregion
    }
}