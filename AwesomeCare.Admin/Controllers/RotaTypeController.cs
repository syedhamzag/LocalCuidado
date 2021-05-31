using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.ViewModels.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Admin.Controllers
{
    public class RotaTypeController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        public RotaTypeController(IClientRotaTypeService clientRotaTypeService, IFileUpload fileUpload):base(fileUpload)
        {
            _clientRotaTypeService = clientRotaTypeService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var model = new ClientRotaTypeViewModel();
            var rotaTypess = await _clientRotaTypeService.Get();
            if (id.HasValue)
            {
                model.SubTitle = "Update RotaType";
                var rotaType = await _clientRotaTypeService.Get(id.Value);
               
                model.ClientRotaTypeId = rotaType.ClientRotaTypeId;
                model.RotaType = model.RotaType;

            }

            model.RotaTypes = rotaTypess;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(ClientRotaTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var rotaTypes = await _clientRotaTypeService.Get();
                model.RotaTypes = rotaTypes;
                return View(model);
            }

            if (model.ClientRotaTypeId == 0)
            {
                var postRotaType = new PostClientRotaType
                {                   
                    Deleted = false,
                    RotaType = model.RotaType
                };
                var result = await _clientRotaTypeService.Post(postRotaType);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "RotaType successfully added" : "An Error Occurred" });

            }
            else
            {
                var putRotaType = new PutClientRotaType
                {
                    Deleted = model.Deleted,
                    RotaType = model.RotaType,
                    ClientRotaTypeId = model.ClientRotaTypeId
                };
                var result = await _clientRotaTypeService.Put(putRotaType);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Rota successfully updated" : "An Error Occurred" });

            }
            return RedirectToAction("Index");
        }
    }
}