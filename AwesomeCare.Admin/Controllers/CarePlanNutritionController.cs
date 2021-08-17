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

        public CarePlanNutritionController(IFileUpload fileUpload, ICarePlanNutritionService nutritionService, IClientService clientService) : base(fileUpload)
        {
            _nutritionService = nutritionService;
            _clientService = clientService;
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
    }
}
