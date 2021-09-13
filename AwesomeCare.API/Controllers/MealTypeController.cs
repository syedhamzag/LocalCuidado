using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        private IGenericRepository<ClientMealType> _clientMealTypeRepository;

        public MealTypeController(IGenericRepository<ClientMealType> clientMealTypeRepository)
        {
            _clientMealTypeRepository = clientMealTypeRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNutrition([FromBody] ClientMealType model)
        {

            var has = _clientMealTypeRepository.Table.Any(c => c.ClientMeal == model.ClientMeal);
            if (has)
            {
                ModelState.AddModelError("", "Client already has Meal Type created");
                return BadRequest(model);
            }
            var result = await _clientMealTypeRepository.InsertEntity(model);
            return Ok(result);
        }
    }
}
