using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.CuidiBuddy;
using AwesomeCare.Admin.ViewModels.CuidiBuddy;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.CuidiBuddy;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CuidiBuddyController : BaseController
    {
        private ICuidiBuddyServices _cuidiBuddyServices;
        private IClientService _clientServices;
        public CuidiBuddyController(ICuidiBuddyServices cuidiBuddyServices, IFileUpload fileUpload, IClientService clientServices) : base(fileUpload)
        {
            _cuidiBuddyServices = cuidiBuddyServices;
            _clientServices = clientServices;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _cuidiBuddyServices.Get();
            return View(entities);
        }

        public IActionResult Index(int clientId)
        {
            var model = new CreateCuidiBuddy();
            model.ClientId = clientId;
            return View(model);
        }
        public async Task<IActionResult> View(int hobbiesId)
        {
            var entity = await _cuidiBuddyServices.Get(hobbiesId);
            var putEntity = Mapper.Map<CreateCuidiBuddy>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int hobbiesId)
        {
            var entity = await _cuidiBuddyServices.Get(hobbiesId);
            var putEntity = Mapper.Map<CreateCuidiBuddy>(entity);
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCuidiBuddy model)
        {
            var clients = await _cuidiBuddyServices.GetCuidi();
            List<int> CuidiIds = clients.Where(s => s.ClientId == model.ClientId).SingleOrDefault().GetCuidiBuddy.Select(s => s.CuidiBuddyId).ToList();
            foreach (GetClient client in clients)
            {
                DateTime bday = DateTime.Parse(client.DateOfBirth);
                int age = (int)((DateTime.Now - bday).TotalDays / 365.242199);
                if (client.TeritoryId == model.Location && client.GenderId == model.Gender && age >= model.AgeFrom && age <= model.AgeTo)
                {

                    if (CuidiIds.Any(s => s != client.ClientId))
                    {
                        model.getClients.Add(client);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public JsonResult Add(int clientId, int cuidiId)
        {
            PostCuidiBuddy entity = new PostCuidiBuddy();
            entity.CuidiBuddyId = cuidiId;
            entity.ClientId = clientId;

            var result = _cuidiBuddyServices.Post(entity);
            var content = result.Result.Content.ReadAsStringAsync();
            return Json(content);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateCuidiBuddy model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PutCuidiBuddy entity = new PutCuidiBuddy();
            entity.CuidiBuddyId = model.CuidiBuddyId;
            entity.ClientId = model.ClientId;

            var result = await _cuidiBuddyServices.Put(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
