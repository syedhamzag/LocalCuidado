using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Pets;
using AwesomeCare.Admin.ViewModels.CarePlan.InterestObjective;
using AwesomeCare.DataTransferObject.DTOs.Pets;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PetsController : BaseController
    {
        private IPetsService _petsService;
        private IClientService _clientService;

        public PetsController(IPetsService petsService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _petsService = petsService;
            _clientService = clientService;
        }

        //public async Task<IActionResult> Reports()
        //{
        //    var entities = await _petsService.Get();

        //    var client = await _clientService.GetClientDetail();
        //    List<CreatePets> reports = new List<CreatePets>();
        //    foreach (GetPets item in entities)
        //    {
        //        var report = new CreatePets();
        //        report.PetsId = item.PetsId;
        //        report.Description = item.Description;
        //        report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
        //        report.Status = item.Status;
        //        reports.Add(report);
        //    }
        //    return View(reports);
        //}

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreatePets();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }

        //public async Task<IActionResult> View(int PetsId)
        //{
        //    var putEntity = await GetPets(PetsId);
        //    return View(putEntity);
        //}

        //public async Task<IActionResult> Edit(int PetsId)
        //{
        //    var putEntity = await GetPets(PetsId);
        //    return View(putEntity);
        //}

        //public async Task<CreatePets> GetPets(int PetsId)
        //{
        //    var Pets = await _petsService.Get(PetsId);
        //    var putEntity = new CreatePets
        //    {
        //        Description = Pets.Description,
        //        Mobility = Pets.Mobility,
        //        PetsId = Pets.PetsId,
        //        ClientId = Pets.ClientId,
        //        Name = Pets.Name,
        //        Status = Pets.Status,
        //    };
        //    return putEntity;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePets model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostPets post = new PostPets();

            post.PetsId = model.PetsId;
            post.ClientId = model.ClientId;
            post.Name = model.Name;
            post.Type = model.Type;
            post.Gender = model.Gender;
            post.Age = model.Age;
            post.PetActivities = model.PetActivities;
            post.MealStorage = model.MealStorage;
            post.VetVisit = model.VetVisit;
            post.PetInsurance = model.PetInsurance;
            post.PetCare = model.PetCare;
            post.MealPattern = model.MealPattern;

            var json = JsonConvert.SerializeObject(post);
            var result = await _petsService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Pets successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreatePets model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var client = await _clientService.GetClient(model.ClientId);
        //        model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
        //        return View(model);
        //    }

        //    PutPets put = new PutPets();

        //    put.PetsId = model.PetsId;
        //    put.ClientId = model.ClientId;
        //    put.Description = model.Description;
        //    put.Mobility = model.Mobility;
        //    put.Name = model.Name;
        //    put.Status = model.Status;

        //    var entity = await _petsService.Put(put);
        //    SetOperationStatus(new Models.OperationStatus
        //    {
        //        IsSuccessful = entity.IsSuccessStatusCode,
        //        Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
        //    });
        //    if (entity.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Reports");
        //    }
        //    return View(model);

        //}
    }
}
