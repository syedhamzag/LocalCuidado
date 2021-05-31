using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.ViewModels.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Services.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using AwesomeCare.Admin.Services.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Extensions;

namespace AwesomeCare.Admin.Controllers
{
    public class RotaController : BaseController
    {
        private readonly IClientRotaNameService _clientRotaService;
        private readonly IBaseRecordService baseRecordService;

        public RotaController(IClientRotaNameService clientRotaService, 
            IFileUpload fileUpload,
            IBaseRecordService baseRecordService) :base(fileUpload)
        {
            _clientRotaService = clientRotaService;
            this.baseRecordService = baseRecordService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var model = new ClientRotaViewModel();
           // model.Cache = cache;
           // model.GetGendersFromCache();

            var rotas = await _clientRotaService.Get();
            var baseRecords = await baseRecordService.GetBaseRecordsWithItems();
            model.Genders = baseRecords.FirstOrDefault(i => i.KeyName == "Gender").BaseRecordItems.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(b.ValueName, b.ValueName)).ToList();
           
            HttpContext.Session.Set<List<SelectListItem>>("genders", model.Genders);

            if (id.HasValue)
            {
                model.SubTitle = "Update Rota";
                var rota = await _clientRotaService.Get(id.Value);
                model.Area = rota.Area;
                model.Gender = rota.Gender;
                model.NumberOfStaff = rota.NumberOfStaff;
                model.RotaId = rota.RotaId;
                model.RotaName = rota.RotaName;

                model.Genders.ForEach(r =>
                {
                    r.Selected = r.Value == rota.Gender;
                });
            }

            model.Rotas = rotas;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ClientRotaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var rotas = await _clientRotaService.Get();
                model.Rotas = rotas;
                model.Genders = HttpContext.Session.Get<List<SelectListItem>>("genders");
                return View(model);
            }

            if (model.RotaId == 0)
            {
                var postRota = new PostClientRotaName
                {
                    Area = model.Area,
                    Deleted = false,
                    Gender = model.Gender,
                    NumberOfStaff = model.NumberOfStaff,
                    RotaName = model.RotaName
                };
               // var kk = JsonConvert.SerializeObject(postRota);
                var result = await _clientRotaService.Post(postRota);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Rota successfully added" : "An Error Occurred" });

            }
            else
            {
                var putRota = new PutClientRotaName
                {
                    Area = model.Area,
                    Deleted = model.Deleted,
                    Gender = model.Gender,
                    NumberOfStaff = model.NumberOfStaff,
                    RotaName = model.RotaName,
                    RotaId = model.RotaId
                };
                var result = await _clientRotaService.Put(putRota);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Rota successfully updated" : "An Error Occurred" });

            }
            return RedirectToAction("Index");
        }

        
    }
}