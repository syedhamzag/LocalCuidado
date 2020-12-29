using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{

    /// <summary>
    /// Managing Tasks for Rota
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RotaTaskController : ControllerBase
    {

        private IGenericRepository<RotaTask> _rotaTaskRepository;
        public RotaTaskController(IGenericRepository<RotaTask> rotaTaskRepository)
        {
            _rotaTaskRepository = rotaTaskRepository;
        }

        /// <summary>
        /// Get RotaTask by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRotaTaskById")]
        [ProducesResponseType(type: typeof(GetRotaTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _rotaTaskRepository.Table.ProjectTo<GetRotaTask>().FirstOrDefault(d => d.RotaTaskId == id && !d.Deleted);

            return Ok(getEntity);
        }
        /// <summary>
        /// Create RotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetRotaTask), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostRotaTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isRotaTaskRegistered = _rotaTaskRepository.Table.Any(r => r.TaskName.ToLower() == model.TaskName.ToLower());
            if (isRotaTaskRegistered)
            {
                return BadRequest($"Rota Task {model.TaskName} already exist");
            }
            var postEntity = Mapper.Map<RotaTask>(model);
            var newEntity = await _rotaTaskRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetRotaTask>(newEntity);

            return CreatedAtRoute("GetRotaTaskById", new { id = getEntity.RotaTaskId }, getEntity);
        }

        /// <summary>
        /// Get all RotaTasks
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetRotaTask>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _rotaTaskRepository.Table.Where(r => !r.Deleted).ProjectTo<GetRotaTask>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Update RotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetRotaTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutRotaTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _rotaTaskRepository.GetEntity(model.RotaTaskId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _rotaTaskRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetRotaTask>(updateEntity);

            return Ok(getEntity);


        }
    }
}