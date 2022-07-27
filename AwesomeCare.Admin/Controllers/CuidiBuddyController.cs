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

        public IActionResult Index()
        {
            var model = new CreateCuidiBuddy();
            return View(model);
        }
        public IActionResult Search()
        {
            var model = new CreateCuidiBuddy();
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
        public async Task<IActionResult> Search(CreateCuidiBuddy model)
        {
            var clients = await _clientServices.GetClients();
            foreach (GetClient client in clients)
            {
                DateTime bday = DateTime.Parse(client.DateOfBirth);
                int age = (int)((DateTime.Now - bday).TotalDays / 365.242199);
                if (client.TeritoryId == model.Location && client.GenderId == model.Gender && age >= model.AgeFrom && age <= model.AgeTo)
                {
                    model.getClients.Add(client);
                }

            }
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCuidiBuddy model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostCuidiBuddy entity = new PostCuidiBuddy();
            entity.CuidiBuddyId = model.CuidiBuddyId;
            entity.ClientId = model.ClientId;

            var result = await _cuidiBuddyServices.Post(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);
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
