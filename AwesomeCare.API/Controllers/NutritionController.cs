using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientMealType;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private ILogger<NutritionController> _logger;
        private IGenericRepository<ClientNutrition> _clientNutritionRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientMealType> _clientMealTypeRepository;

        public NutritionController(ILogger<NutritionController> logger, IGenericRepository<ClientNutrition> clientNutritionRepository, 
            AwesomeCareDbContext dbContext, IGenericRepository<ClientMealType> clientMealTypeRepository)
        {
            _logger = logger;
            _clientNutritionRepository = clientNutritionRepository;
            _clientMealTypeRepository = clientMealTypeRepository;
            _dbContext = dbContext;
        }
        ///// <summary>
        ///// Get all Client MealTypes
        ///// </summary>
        ///// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientMealType>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {

            var getEntities = _clientMealTypeRepository.Table.Where(r => !r.Deleted).ProjectTo<GetClientMealType>().ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Get Client Nutrition for Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetForEdit/{id}")]
        [ProducesResponseType(type: typeof(List<GetClientNutrition>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetForEdit(int id)
        {
            var Nutrition = GetClientNutrition(id);
            return Ok(Nutrition);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit([FromBody] CreateNutrition model, int id)
        {
            var clientMealEntity = _dbContext.Set<ClientNutrition>();
            var Nutrition = clientMealEntity.Where(c => c.ClientId == id).Include(d => d.ClientMealDays)
                .Include(s => s.ClientShopping).Include(s => s.ClientCleaning).Include(s=>s.ClientMealDays).ToList(); //    GetClientMeal(id);//from database
            
            Nutrition.FirstOrDefault().ShoppingSpecialNote = model.ShoppingSpecialNote;
            Nutrition.FirstOrDefault().CleaningSpecialNote = model.CleaningSpecialNote;
            Nutrition.FirstOrDefault().DATEFROM = model.DATEFROM;
            Nutrition.FirstOrDefault().DATETO = model.DATETO;
            Nutrition.FirstOrDefault().MealSpecialNote = model.MealSpecialNote;
            Nutrition.FirstOrDefault().StaffId = model.StaffId;
            
            _dbContext.Entry(Nutrition.FirstOrDefault()).State = EntityState.Modified;
            
            foreach (var Meal in Nutrition.FirstOrDefault().ClientMealDays.ToList())
            {

                //check if Meal in db is part of the Meals in Model if not mark as Deleted
                var currentMeal = model.ClientMealDays.Select(s=>s).Where(s=>s.ClientMealTypeId == Meal.ClientMealTypeId);
                if (currentMeal == null)
                {
                    //delete from database
                    _dbContext.Entry(Meal).State = EntityState.Deleted;
                }
                else
                {
                    //Meal in Db is present in Model
                    //var currentDbRotaDay = currentRota.ClientRotaDays.FirstOrDefault(d => d.ClientRotaDaysId == rotaDayDb.ClientRotaDaysId);
                    var currentDbMeal = currentMeal.FirstOrDefault(s=>s.MealDayofWeekId == Meal.MealDayofWeekId);
                    if (currentDbMeal != null)
                    {
                        Meal.MealDayofWeekId = currentDbMeal.MealDayofWeekId;
                        Meal.MEALDETAILS = currentDbMeal.MEALDETAILS;
                        Meal.HOWTOPREPARE = currentDbMeal.HOWTOPREPARE;
                        Meal.TypeId = currentDbMeal.TypeId;
                        Meal.SEEVIDEO = currentDbMeal.SEEVIDEO;
                        Meal.ClientMealTypeId = currentDbMeal.ClientMealTypeId;
                        Meal.PICTURE = Meal.PICTURE;
                        Meal.NutritionId = Meal.NutritionId;

                        _dbContext.Entry(Meal).State = EntityState.Modified;
                    }
                }
            }
            foreach (var Shop in Nutrition.FirstOrDefault().ClientShopping)
            {
                //check if Meal in db is part of the Meals in Model if not mark as Deleted
                var current = model.ClientShopping.FirstOrDefault(r => r.ShoppingId == Shop.ShoppingId);
                if (current == null)
                {
                    //delete from database
                    _dbContext.Entry(Shop).State = EntityState.Deleted;
                }
                else
                {
                    //Shopping in Db is present in Model
                    Shop.Amount = current.Amount;
                    Shop.DATEFROM = current.DATEFROM;
                    Shop.DATETO = current.DATETO;
                    Shop.DAYOFSHOPPING = current.DAYOFSHOPPING;
                    Shop.Description = current.Description;
                    Shop.Item = current.Item;
                    Shop.LocationOfPurchase = current.LocationOfPurchase;
                    Shop.MeansOfPurchase = current.MeansOfPurchase;
                    Shop.Quantity = current.Quantity;
                    Shop.STAFFId = current.STAFFId;
                    Shop.Image = Shop.Image;
                    Shop.NutritionId = Shop.NutritionId;

                    _dbContext.Entry(Shop).State = EntityState.Modified;
                }
            }
            foreach (var Clean in Nutrition.FirstOrDefault().ClientCleaning)
            {
                //check if Meal in db is part of the Meals in Model if not mark as Deleted
                var current = model.ClientCleaning.FirstOrDefault(r => r.CleaningId == Clean.CleaningId);
                if (current == null)
                {
                    //delete from database
                    _dbContext.Entry(Clean).State = EntityState.Deleted;
                }
                else
                {
                    //Cleaning in Db is present in Model
                    Clean.AreasAndItems = current.AreasAndItems;
                    Clean.DATEFROM = current.DATEFROM;
                    Clean.DATETO = current.DATETO;
                    Clean.DAYOFCLEANING = current.DAYOFCLEANING;
                    Clean.DescOfItem = current.DescOfItem;
                    Clean.Details = current.Details;
                    Clean.Disposal = current.Disposal;
                    Clean.LocationOfItem = current.LocationOfItem;
                    Clean.MinuteAlloted = current.MinuteAlloted;
                    Clean.SafetyHazard = current.SafetyHazard;
                    Clean.SEEVIDEO = current.SEEVIDEO;
                    Clean.WhereToGet = current.WhereToGet;
                    Clean.STAFFId = current.STAFFId;
                    Clean.Image = Clean.Image;
                    Clean.WhereToKeep = Clean.WhereToKeep;
                    Clean.NutritionId = Clean.NutritionId;

                    _dbContext.Entry(Clean).State = EntityState.Modified;
                }
            }

            //Meals from Model not in Database
            foreach (var item in model.ClientMealDays)
            {
                var MealNotInDb = Nutrition[0].ClientMealDays.Where(r => r.ClientMealId == item.ClientMealId && r.MealDayofWeekId == item.MealDayofWeekId).SingleOrDefault();
                if (MealNotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientMealDays>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            //Shopping from Model not in Database
            foreach (var item in model.ClientShopping)
            {
                var MealNotInDb = Nutrition[0].ClientShopping.SingleOrDefault(r => r.ShoppingId == item.ShoppingId);
                if (MealNotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientShopping>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            //Cleaning from Model not in Database
            foreach (var item in model.ClientCleaning)
            {
                var MealNotInDb = Nutrition[0].ClientCleaning.SingleOrDefault(r => r.CleaningId == item.CleaningId);
                if (MealNotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientCleaning>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }

        List<GetClientNutrition> GetClientNutrition(int clientId)
        {
            var Nutrition = (from c in _clientNutritionRepository.Table
                             where c.ClientId == clientId
                             select new GetClientNutrition
                             {
                                 ClientId = c.ClientId,
                                 NutritionId = c.NutritionId,
                                 StaffId = c.StaffId,
                                 MealSpecialNote = c.MealSpecialNote,
                                 ShoppingSpecialNote = c.ShoppingSpecialNote,
                                 CleaningSpecialNote = c.CleaningSpecialNote,
                                 DATEFROM = c.DATEFROM,
                                 DATETO = c.DATETO,
                                 ClientMealDays = (from d in c.ClientMealDays
                                                   select new GetClientMealDays
                                                   {
                                                       ClientMealTypeId = d.ClientMealTypeId,
                                                       ClientMealId = d.ClientMealId,
                                                       MealDayofWeekId = d.MealDayofWeekId,
                                                       MEALDETAILS = d.MEALDETAILS,
                                                       HOWTOPREPARE = d.HOWTOPREPARE,
                                                       TypeId = d.TypeId,
                                                       SEEVIDEO = d.SEEVIDEO,
                                                       PICTURE = d.PICTURE,
                                                       NutritionId = d.NutritionId
                                                   }).ToList(),
                                 ClientCleaning = (from cc in c.ClientCleaning
                                                   select new GetClientCleaning
                                                   {
                                                       CleaningId = cc.CleaningId,
                                                       NutritionId = cc.NutritionId,
                                                       STAFFId = cc.STAFFId,
                                                       AreasAndItems = cc.AreasAndItems,
                                                       Details = cc.Details,
                                                       SafetyHazard = cc.SafetyHazard,
                                                       LocationOfItem = cc.LocationOfItem,
                                                       DescOfItem = cc.DescOfItem,
                                                       MinuteAlloted = cc.MinuteAlloted,
                                                       Disposal = cc.Disposal,
                                                       WhereToGet = cc.WhereToGet,
                                                       WhereToKeep = cc.WhereToKeep,
                                                       SEEVIDEO = cc.SEEVIDEO,
                                                       Image = cc.Image,
                                                       DAYOFCLEANING = cc.DAYOFCLEANING,
                                                       DATEFROM = cc.DATEFROM,
                                                       DATETO = cc.DATETO
                                                   }).ToList(),
                                 ClientShopping = (from s in c.ClientShopping
                                                   select new GetClientShopping
                                                   {
                                                       ShoppingId = s.ShoppingId,
                                                       NutritionId = s.NutritionId,
                                                       STAFFId = s.STAFFId,
                                                       MeansOfPurchase = s.MeansOfPurchase,
                                                       LocationOfPurchase = s.LocationOfPurchase,
                                                       Item = s.Item,
                                                       Description = s.Description,
                                                       Quantity = s.Quantity,
                                                       Amount = s.Amount,
                                                       Image = s.Image,
                                                       DAYOFSHOPPING = s.DAYOFSHOPPING,
                                                       DATEFROM = s.DATEFROM,
                                                       DATETO = s.DATETO
                                                   }).ToList(),
                             }).ToList();

            return Nutrition;
        }
        /// <summary>
        /// Create Client Nutrition
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNutrition([FromBody] CreateNutrition model)
        {
            var clientId = model.ClientId;
            var hasNutrition = _clientNutritionRepository.Table.Any(c => c.ClientId == clientId);
            if (hasNutrition)
            {
                ModelState.AddModelError("", "Client already as nutrition created");
                return BadRequest(model);
            }
            var postEntity = Mapper.Map<ClientNutrition>(model);
            var result = await _clientNutritionRepository.InsertEntity(postEntity);
            var nutrition = Mapper.Map<GetClientNutrition>(result);

            return Ok(nutrition);
        }
    }
}