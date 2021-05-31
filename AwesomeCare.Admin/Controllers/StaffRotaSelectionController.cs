using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffRotaSelectionController: BaseController
    {
        IStaffService _staffService;
        public StaffRotaSelectionController(IStaffService staffService, IFileUpload fileUpload) : base(fileUpload)
        {
            _staffService = staffService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var model = new CreateStaffRotaSelection();
            var rotaSelections = await _staffService.GetRotaSelections();
            if (id.HasValue)
            {
                model.SubTitle = "Update Item";
                var rotaType = await _staffService.GetRotaSelection(id.Value);

                model.StaffRotaDynamicAdditionId = rotaType.StaffRotaDynamicAdditionId;
                model.ItemName = model.ItemName;

            }

            model.RotaSelections = rotaSelections;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(CreateStaffRotaSelection model)
        {
            if (!ModelState.IsValid)
            {
                var items = await _staffService.GetRotaSelections();
                model.RotaSelections = items;
                return View(model);
            }

            if (model.StaffRotaDynamicAdditionId == 0)
            {
                var postItem = new PostStaffRotaDynamicAddition
                {
                    Deleted = false,
                    ItemName = model.ItemName
                };
                var result = await _staffService.CreateRotaSelection(postItem);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Item successfully added" : "An Error Occurred" });

            }
            else
            {
                var putRotaType = new PutStaffRotaDynamicAddition
                {
                    Deleted = model.Deleted,
                    ItemName = model.ItemName,
                    StaffRotaDynamicAdditionId = model.StaffRotaDynamicAdditionId
                };
                var result = await _staffService.UpdateRotaSelection(putRotaType);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Item successfully updated" : "An Error Occurred" });

            }
            return RedirectToAction("Index");
        }
    }
}
