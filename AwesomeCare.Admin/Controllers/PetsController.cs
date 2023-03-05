using AwesomeCare.Admin.Services.Admin;
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
        private IBaseRecordService _baseService;

        public PetsController(IPetsService petsService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService ) : base(fileUpload)
        {
            _petsService = petsService;
            _clientService = clientService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _petsService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreatePets> reports = new List<CreatePets>();
            foreach (GetPets item in entities)
            {
                var report = new CreatePets();
                report.PetsId = item.PetsId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.Name = item.Name;
                report.TypeName = _baseService.GetBaseRecordItemById(item.Type).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreatePets();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var entity = await _petsService.GetbyClient(clientId);
            if (entity != null)
            {
                model = GetPets(entity);
            }
            return View(model);

        }

        public async Task<IActionResult> Delete(int clientId)
        {
            var sp = await _petsService.GetbyClient(clientId);
            await _petsService.Delete(sp.PetsId);
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = clientId });
        }

        public async Task<IActionResult> View(int clientId)
        {
            var pets = await _petsService.GetbyClient(clientId);
            var putEntity = GetPets(pets);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int petsId)
        {
            var pets = await _petsService.Get(petsId);
            var putEntity = GetPets(pets);
            return View(putEntity);
        }

        public CreatePets GetPets(GetPets Pets)
        {
            
            var putEntity = new CreatePets
            {
                Age = Pets.Age,
                Type = Pets.Type,
                PetsId = Pets.PetsId,
                ClientId = Pets.ClientId,
                Name = Pets.Name,
                Gender = Pets.Gender,
                PetActivities = Pets.PetActivities,
                PetCare = Pets.PetCare,
                MealPattern = Pets.MealPattern,
                PetInsurance = Pets.PetInsurance,
                MealStorage = Pets.MealStorage,
                VetVisit = Pets.VetVisit,
                Title = "Update Pets",
                ActionName = "Update",
                Method = "Edit"
            };
            return putEntity;
        }

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

            var result = await _petsService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Pets successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePets model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutPets put = new PutPets();

            put.Age = model.Age;
            put.Type = model.Type;
            put.PetsId = model.PetsId;
            put.ClientId = model.ClientId;
            put.Name = model.Name;
            put.Gender = model.Gender;
            put.PetActivities = model.PetActivities;
            put.PetCare = model.PetCare;
            put.MealPattern = model.MealPattern;
            put.PetInsurance = model.PetInsurance;
            put.MealStorage = model.MealStorage;
            put.VetVisit = model.VetVisit;

            var entity = await _petsService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }
    }
}
