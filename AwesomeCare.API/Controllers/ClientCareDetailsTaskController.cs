using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientCareDetailsTaskController : ControllerBase
    {
        private IGenericRepository<ClientCareDetailsTask> _clientCareDetailsTaskRepository;
        private ILogger<ClientCareDetailsTaskController> _logger;

        public ClientCareDetailsTaskController(IGenericRepository<ClientCareDetailsTask> clientCareDetailsTaskRepository, ILogger<ClientCareDetailsTaskController> logger)
        {
            _clientCareDetailsTaskRepository = clientCareDetailsTaskRepository;
            _logger = logger;
        }


        /// <summary>
        /// Get ClientCareDetailsTask by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientCareDetailsTaskById")]
        [ProducesResponseType(type: typeof(GetClientCareDetailsTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientCareDetailsTaskRepository.Table.ProjectTo<GetClientCareDetailsTask>().FirstOrDefault(d => d.ClientCareDetailsTaskId == id  && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All ClientCareDetailsTask
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientCareDetailsTask>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientCareDetailsTaskRepository.Table.ProjectTo<GetClientCareDetailsTask>().ToList();
            return Ok(getEntities);
        }


        /// <summary>
        /// Create ClientCareDetailsTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientCareDetailsTask), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientCareDetailsTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<ClientCareDetailsTask>(model);
            var newEntity = await _clientCareDetailsTaskRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientCareDetailsTask>(newEntity);

            return CreatedAtRoute("GetClientCareDetailsTaskById", new { id = getEntity.ClientCareDetailsTaskId }, getEntity);
        }


        /// <summary>
        /// Update ClientCareDetailsTask. Can also be used to delete a record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientCareDetailsTask model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<ClientCareDetailsTask>(model);
            var newEntity = await _clientCareDetailsTaskRepository.UpdateEntity(postEntity);

            return Ok(newEntity);
        }
    }
}