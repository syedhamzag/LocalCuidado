using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientCareDetailsHeadingController : ControllerBase
    {
        private IGenericRepository<ClientCareDetailsHeading> _clientCareDetailsHeadingRepository;
        private ILogger<ClientCareDetailsHeadingController> _logger;
        private IDbContext _dbContext;
        public ClientCareDetailsHeadingController(IDbContext dbContext, IGenericRepository<ClientCareDetailsHeading> clientCareDetailsHeadingRepository, ILogger<ClientCareDetailsHeadingController> logger)
        {
            _clientCareDetailsHeadingRepository = clientCareDetailsHeadingRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get ClientCareDetailsHeading by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientCareDetailsHeadingId")]
        [ProducesResponseType(type: typeof(GetClientCareDetailsHeading), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientCareDetailsHeadingRepository.Table.ProjectTo<GetClientCareDetailsHeading>().FirstOrDefault(d => d.ClientCareDetailsHeadingId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get ClientCareDetailsHeading with Tasks by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHeadingwithTasks/{id}", Name = "GetClientCareDetailsHeadingWithTasksById")]
        [ProducesResponseType(type: typeof(GetClientCareDetailsHeadingWithTasks), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetHeadingWithTasks(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientCareDetailsHeadingRepository.Table.ProjectTo<GetClientCareDetailsHeadingWithTasks>().FirstOrDefault(d => d.ClientCareDetailsHeadingId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get ClientCareDetailsHeadings with Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHeadingsWithTasks", Name = "GetClientCareDetailsHeadingsWithTasks")]
        [ProducesResponseType(type: typeof(List<GetClientCareDetailsHeadingWithTasks>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetHeadingsWithTasks()
        {

            var getEntities = _clientCareDetailsHeadingRepository.Table.ProjectTo<GetClientCareDetailsHeadingWithTasks>().ToList();

            return Ok(getEntities);
        }


        /// <summary>
        /// Get All ClientCareDetailsHeading
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientCareDetailsHeading>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientCareDetailsHeadingRepository.Table.Where(c => !c.Deleted).ProjectTo<GetClientCareDetailsHeading>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientCareDetailsHeading
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientCareDetailsHeading), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientCareDetailsHeading model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isEntityRegistered = _clientCareDetailsHeadingRepository.Table.Any(r => r.Heading.Equals(model.Heading, StringComparison.InvariantCultureIgnoreCase));
            if (isEntityRegistered)
            {
                return BadRequest($"Care Details {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<ClientCareDetailsHeading>(model);
            var newEntity = await _clientCareDetailsHeadingRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientCareDetailsHeading>(newEntity);

            return CreatedAtRoute("GetClientCareDetailsHeadingId", new { id = getEntity.ClientCareDetailsHeadingId }, getEntity);
        }


        /// <summary>
        /// Create Client CareDetails Heading with Tasks
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostHeadingwithTasks")]
        public async Task<IActionResult> PostHeadingWithTasks([FromBody]PostClientCareDetailsHeadingWithTasks model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _clientCareDetailsHeadingRepository.Table.Any(r => r.Heading.Equals(model.Heading, StringComparison.InvariantCultureIgnoreCase));
            if (isEntityRegistered)
            {
                return BadRequest($"Care Details {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<ClientCareDetailsHeading>(model);
            var newEntity = await _clientCareDetailsHeadingRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientCareDetailsHeading>(newEntity);

            return CreatedAtRoute("GetClientCareDetailsHeadingId", new { id = getEntity.ClientCareDetailsHeadingId }, getEntity);
        }

        /// <summary>
        /// Update ClientCareDetailsHeading. Can also delete Heading with corresponding Tasks
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientCareDetailsHeading), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientCareDetailsHeading model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var careDetailsTaskEntity = _dbContext.Set<ClientCareDetailsTask>();

                var careDetailsTasks = careDetailsTaskEntity.Where(c => c.ClientCareDetailsHeadingId == model.ClientCareDetailsHeadingId).ToList();
                var putEntity = Mapper.Map<ClientCareDetailsHeading>(model);

                foreach (var task in careDetailsTasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetClientCareDetailsHeading>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<ClientCareDetailsHeading>(model);
                var newEntity = await _clientCareDetailsHeadingRepository.UpdateEntity(postEntity);
                var getEntity = Mapper.Map<GetClientCareDetailsHeading>(newEntity);
                return Ok(getEntity);
            }



        }
    }
}