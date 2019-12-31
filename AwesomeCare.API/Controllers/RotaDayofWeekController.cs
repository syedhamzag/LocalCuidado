using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RotaDayofWeekController : ControllerBase
    {
        private IGenericRepository<RotaDayofWeek> _rotaDayofWeekRepository;
        public RotaDayofWeekController(IGenericRepository<RotaDayofWeek> rotaDayofWeekRepository)
        {
            _rotaDayofWeekRepository = rotaDayofWeekRepository;
        }

        /// <summary>
        /// Get RotaDayofWeek by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRotaDayofWeekById")]
        [ProducesResponseType(type: typeof(GetRotaDayofWeek), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _rotaDayofWeekRepository.Table.ProjectTo<GetRotaDayofWeek>().FirstOrDefault(d => d.RotaDayofWeekId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All RotaDayofWeek
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetRotaDayofWeek>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _rotaDayofWeekRepository.Table.Where(r => !r.Deleted).ProjectTo<GetRotaDayofWeek>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create RotaDayofWeek
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetRotaDayofWeek), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostRotaDayofWeek model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isweekDayRegistered = _rotaDayofWeekRepository.Table.Any(r => r.DayofWeek.Equals(model.DayofWeek, StringComparison.InvariantCultureIgnoreCase));
            if (isweekDayRegistered)
            {
                return BadRequest($"Week Day {model.DayofWeek} already exist");
            }
            var postEntity = Mapper.Map<RotaDayofWeek>(model);
            var newEntity = await _rotaDayofWeekRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetRotaDayofWeek>(newEntity);

            return CreatedAtRoute("GetRotaDayofWeekById", new { id = getEntity.RotaDayofWeekId }, getEntity);
        }

        /// <summary>
        /// Update RotaDayofWeek
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetRotaDayofWeek), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutRotaDayofWeek model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _rotaDayofWeekRepository.GetEntity(model.RotaDayofWeekId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _rotaDayofWeekRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetRotaDayofWeek>(updateEntity);

            return Ok(getEntity);


        }
    }
}