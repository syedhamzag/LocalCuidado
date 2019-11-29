using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaTaskController : ControllerBase
    {
        private IGenericRepository<ClientRotaTask> _clientRotaTaskRepository;
        public ClientRotaTaskController(IGenericRepository<ClientRotaTask> clientRotaTaskRepository)
        {
            _clientRotaTaskRepository = clientRotaTaskRepository;
        }

        /// <summary>
        /// Get ClientRotaTask by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaTaskById")]
        [ProducesResponseType(type: typeof(GetClientRotaTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientRotaTaskRepository.Table.ProjectTo<GetClientRotaTask>().FirstOrDefault(d => d.ClientRotaTaskId == id);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All ClientRotaTasks
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRotaTask>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientRotaTaskRepository.Table.ProjectTo<GetClientRotaTask>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientRotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientRotaTask), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientRotaTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var postEntity = Mapper.Map<ClientRotaTask>(model);
            var newEntity = await _clientRotaTaskRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientRotaTask>(newEntity);

            return CreatedAtRoute("GetClientRotaTaskById", new { id = getEntity.ClientRotaTaskId }, getEntity);
        }

        /// <summary>
        /// Update ClientRotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRotaTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRotaTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientRotaTaskRepository.GetEntity(model.ClientRotaTaskId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientRotaTaskRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientRotaTask>(updateEntity);

            return Ok(getEntity);


        }
    }
}