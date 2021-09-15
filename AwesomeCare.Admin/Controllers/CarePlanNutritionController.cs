using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.CarePlanNutrition;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.ViewModels.CarePlan;
using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CarePlanNutritionController : BaseController
    {
        private ICarePlanNutritionService _nutritionService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;

        public CarePlanNutritionController(IFileUpload fileUpload, ICarePlanNutritionService nutritionService, IClientService clientService, IBaseRecordService baseService) : base(fileUpload)
        {
            _nutritionService = nutritionService;
            _clientService = clientService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateCarePlanNutrition();
            model.ClientId = clientId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCarePlanNutrition model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                return View(model);
            }
            PostCarePlanNutrition postlog = new PostCarePlanNutrition();

            postlog.ClientId = model.ClientId;
            postlog.AvoidFood = model.AvoidFood;
            postlog.DrinkType = model.DrinkType;
            postlog.EatingDifficulty = model.EatingDifficulty;
            postlog.FoodIntake = model.FoodIntake;
            postlog.FoodStorage = model.FoodStorage;
            postlog.MealPreparation = model.MealPreparation;
            postlog.RiskMitigations = model.RiskMitigations;
            postlog.ServingMeal = model.ServingMeal;
            postlog.SpecialDiet = model.SpecialDiet;
            postlog.ThingsILike = model.ThingsILike;
            postlog.WhenRestock = model.WhenRestock;
            postlog.WhoRestock = model.WhoRestock;

            var result = await _nutritionService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New CarePlan Nutrition successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        public async Task<IActionResult> View(int NutritionId)
        {
            var putEntity = await GetNutrition(NutritionId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int NutritionId)
        {
            var putEntity = await GetNutrition(NutritionId);
            return View(putEntity);
        }

        public async Task<CreateCarePlanNutrition> GetNutrition(int NutritionId)
        {
            var nutrition = await _nutritionService.Get(NutritionId);
            var putEntity = new CreateCarePlanNutrition
            {
                AvoidFood = nutrition.AvoidFood,
                DrinkType = nutrition.DrinkType,
                EatingDifficulty = nutrition.EatingDifficulty,
                ClientId = nutrition.ClientId,
                FoodIntake = nutrition.FoodIntake,
                FoodStorage = nutrition.FoodStorage,
                MealPreparation = nutrition.MealPreparation,
                NutritionId = nutrition.NutritionId,
                RiskMitigations = nutrition.RiskMitigations,
                ServingMeal = nutrition.ServingMeal,
                SpecialDiet = nutrition.SpecialDiet,
                ThingsILike = nutrition.ThingsILike,
                WhenRestock = nutrition.WhenRestock,
                WhoRestock = nutrition.WhoRestock,
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateCarePlanNutrition model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutCarePlanNutrition put = new PutCarePlanNutrition();

            put.AvoidFood = model.AvoidFood;
            put.DrinkType = model.DrinkType;
            put.EatingDifficulty = model.EatingDifficulty;
            put.ClientId = model.ClientId;
            put.FoodIntake = model.FoodIntake;
            put.FoodStorage = model.FoodStorage;
            put.MealPreparation = model.MealPreparation;
            put.NutritionId = model.NutritionId;
            put.RiskMitigations = model.RiskMitigations;
            put.ServingMeal = model.ServingMeal;
            put.SpecialDiet = model.SpecialDiet;
            put.ThingsILike = model.ThingsILike;
            put.WhenRestock = model.WhenRestock;
            put.WhoRestock = model.WhoRestock;

            var entity = await _nutritionService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _nutritionService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateCarePlanNutrition> reports = new List<CreateCarePlanNutrition>();
            foreach (GetCarePlanNutrition item in entities)
            {
                var report = new CreateCarePlanNutrition();

                report.AvoidFood = item.AvoidFood;
                report.ThingsILikeName = _baseService.GetBaseRecordItemById(item.ThingsILike).Result.ValueName;
                report.NutritionId = item.NutritionId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }
    }
}
