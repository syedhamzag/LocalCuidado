using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRota;
using AwesomeCare.Admin.ViewModels.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class RotaController : BaseController
    {
        private readonly IClientRotaService _clientRotaService;
        public RotaController(IClientRotaService clientRotaService)
        {
            _clientRotaService = clientRotaService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var model = new ClientRotaViewModel();
            var rotas = await _clientRotaService.Get();
            if (id.HasValue)
            {
                model.SubTitle = "Update Rota";
                var rota = await _clientRotaService.Get(id.Value);
                model.Area = rota.Area;
                model.Gender = rota.Gender;
                model.NumberOfStaff = model.NumberOfStaff;
                model.RotaId = rota.RotaId;
                model.RotaName = model.RotaName;

                model.Genders.ForEach(r =>
                {
                    r.Selected = r.Value == model.Gender;
                });
            }

            model.Rotas = rotas;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(ClientRotaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var rotas = await _clientRotaService.Get();
                model.Rotas = rotas;
                return View(model);
            }

            if (model.RotaId == 0)
            {
                var postRota = new PostClientRota
                {
                    Area = model.Area,
                    Deleted = false,
                    Gender = model.Gender,
                    NumberOfStaff = model.NumberOfStaff,
                    RotaName = model.RotaName
                };
                var result = await _clientRotaService.Post(postRota);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Rota successfully added" : "An Error Occurred" });

            }
            else
            {
                var putRota = new PutClientRota
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