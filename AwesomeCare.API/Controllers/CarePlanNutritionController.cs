using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarePlanNutritionController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<CarePlanNutrition> _nutritionRepository;

        public CarePlanNutritionController (AwesomeCareDbContext dbContext, IGenericRepository<CarePlanNutrition> nutritionRepository)
        {
            _nutritionRepository = nutritionRepository;
            _dbContext = dbContext;
        }

        #region Nutrition
        /// <summary>
        /// Get All Nutrition
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetCarePlanNutrition>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _nutritionRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create CarePlanNutrition
        /// </summary>
        /// <param name="postCarePlanNutrition"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostCarePlanNutrition postCarePlanNutrition)
        {
            if (postCarePlanNutrition == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Nutrition = Mapper.Map<CarePlanNutrition>(postCarePlanNutrition);
            await _nutritionRepository.InsertEntity(Nutrition);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanNutrition
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutCarePlanNutrition models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbContext.SaveChanges();
            var nutrition = Mapper.Map<CarePlanNutrition>(models);
            await _nutritionRepository.UpdateEntity(nutrition);
            return Ok();
        }
        /// <summary>
        /// Get CarePlanNutrition by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetCarePlanNutrition), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getnutrition = await (from c in _nutritionRepository.Table
                                            where c.NutritionId == id
                                            select new GetCarePlanNutrition
                                            {
                                                NutritionId = c.NutritionId,
                                                ClientId = c.ClientId,
                                                AvoidFood = c.AvoidFood,
                                                DrinkType = c.DrinkType,
                                                EatingDifficulty = c.EatingDifficulty,
                                                FoodIntake = c.FoodIntake,
                                                FoodStorage = c.FoodStorage,
                                                MealPreparation = c.MealPreparation,
                                                RiskMitigations = c.RiskMitigations,
                                                ServingMeal = c.ServingMeal,
                                                SpecialDiet = c.SpecialDiet,
                                                ThingsILike = c.ThingsILike,
                                                WhenRestock = c.WhenRestock,
                                                WhoRestock = c.WhoRestock,
                                            }
                      ).FirstOrDefaultAsync();
            return Ok(getnutrition);
        }
        /// <summary>
        /// Get CarePlanNutrition by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetCarePlanNutrition), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getnutrition = await (from c in _nutritionRepository.Table
                                      where c.ClientId == id
                                      select new GetCarePlanNutrition
                                      {
                                          NutritionId = c.NutritionId,
                                          ClientId = c.ClientId,
                                          AvoidFood = c.AvoidFood,
                                          DrinkType = c.DrinkType,
                                          EatingDifficulty = c.EatingDifficulty,
                                          FoodIntake = c.FoodIntake,
                                          FoodStorage = c.FoodStorage,
                                          MealPreparation = c.MealPreparation,
                                          RiskMitigations = c.RiskMitigations,
                                          ServingMeal = c.ServingMeal,
                                          SpecialDiet = c.SpecialDiet,
                                          ThingsILike = c.ThingsILike,
                                          WhenRestock = c.WhenRestock,
                                          WhoRestock = c.WhoRestock,
                                      }
                      ).FirstOrDefaultAsync();
            return Ok(getnutrition);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _nutritionRepository.GetEntity(id);
            await _nutritionRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
