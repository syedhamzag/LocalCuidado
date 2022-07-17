using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Pets;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class PetsController : ControllerBase
    {
        private IGenericRepository<Pets> _petsRepository;


        public PetsController(IGenericRepository<Pets> petsRepository)
        {
            _petsRepository = petsRepository;
        }
        #region Pets
        /// <summary>
        /// Get All Pets
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPets>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _petsRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Pets
        /// </summary>
        /// <param name="postInterestGoalPets"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPets postInterestGoalPets)
        {
            if (postInterestGoalPets == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Pets = Mapper.Map<Pets>(postInterestGoalPets);
            await _petsRepository.InsertEntity(Pets);
            return Ok();
        }
        /// <summary>
        /// Update Pets
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostPets models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Pets = Mapper.Map<Pets>(models);
            await _petsRepository.UpdateEntity(Pets);
            return Ok();

        }
        /// <summary>
        /// Get Pets by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPets), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPets = await (from c in _petsRepository.Table
                                           where c.PetsId == id.Value
                                           select new GetPets
                                           {
                                               PetsId = c.PetsId,
                                               Age = c.Age,
                                               ClientId = c.ClientId,
                                               Gender = c.Gender,
                                               Name = c.Name,
                                               MealPattern = c.MealPattern,
                                               MealStorage = c.MealStorage,
                                               PetCare = c.PetCare,
                                               PetInsurance = c.PetInsurance,
                                               Type = c.Type,
                                               PetActivities = c.PetActivities,
                                               VetVisit = c.VetVisit
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getPets);
        }
        /// <summary>
        /// Get Pets by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetPets), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPets = await (from c in _petsRepository.Table
                                 where c.ClientId == id.Value
                                 select new GetPets
                                 {
                                     PetsId = c.PetsId,
                                     Age = c.Age,
                                     ClientId = c.ClientId,
                                     Gender = c.Gender,
                                     Name = c.Name,
                                     MealPattern = c.MealPattern,
                                     MealStorage = c.MealStorage,
                                     PetCare = c.PetCare,
                                     PetInsurance = c.PetInsurance,
                                     Type = c.Type,
                                     PetActivities = c.PetActivities,
                                     VetVisit = c.VetVisit
                                 }
                      ).FirstOrDefaultAsync();
            return Ok(getPets);
        }
        #endregion
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _petsRepository.GetEntity(id);
            await _petsRepository.DeleteEntity(entity);
            return Ok();
        }
    }
}
