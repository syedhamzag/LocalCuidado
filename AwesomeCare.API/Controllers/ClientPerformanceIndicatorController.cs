using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientPerformanceIndicatorController : ControllerBase
    {
        private IGenericRepository<ClientPerformanceIndicator> _ClientPerformanceIndicatorRepository;
        private IGenericRepository<ClientPerformanceIndicatorTask> _ClientPerformanceIndicatorTaskRepository;
        private ILogger<ClientPerformanceIndicatorController> _logger;
        private AwesomeCareDbContext _dbContext;
        public ClientPerformanceIndicatorController(AwesomeCareDbContext dbContext, IGenericRepository<ClientPerformanceIndicator> ClientPerformanceIndicatorRepository, IGenericRepository<ClientPerformanceIndicatorTask> ClientPerformanceIndicatorTaskRepository, ILogger<ClientPerformanceIndicatorController> logger)
        {
            _ClientPerformanceIndicatorRepository = ClientPerformanceIndicatorRepository;
            _logger = logger;
            _dbContext = dbContext;
            _ClientPerformanceIndicatorTaskRepository = ClientPerformanceIndicatorTaskRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientPerformanceIndicator), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _ClientPerformanceIndicatorRepository.Table
                                   where h.PerformanceIndicatorId == id
                                   select new GetClientPerformanceIndicator
                                   {
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       Date = h.Date,
                                       DueDate = h.DueDate,
                                       Rating = h.Rating,
                                       Remarks = h.Remarks,
                                       ClientId = h.ClientId,
                                       GetClientPerformanceIndicatorTask = (from t in h.ClientPerformanceIndicatorTask
                                                                      select new GetClientPerformanceIndicatorTask
                                                                      {
                                                                          PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                          Title = t.Title,
                                                                          Score = t.Score,
                                                                      }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet("GetByClient/{id}")]
        [ProducesResponseType(type: typeof(GetClientPerformanceIndicator), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByClient(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _ClientPerformanceIndicatorRepository.Table
                                   where h.ClientId == id
                                   select new GetClientPerformanceIndicator
                                   {
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       Date = h.Date,
                                       DueDate = h.DueDate,
                                       Rating = h.Rating,
                                       Remarks = h.Remarks,
                                       ClientId = h.ClientId,
                                       GetClientPerformanceIndicatorTask = (from t in h.ClientPerformanceIndicatorTask
                                                                            select new GetClientPerformanceIndicatorTask
                                                                            {
                                                                                PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                                Title = t.Title,
                                                                                Score = t.Score,
                                                                            }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientPerformanceIndicator>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var getEntity = await (from h in _ClientPerformanceIndicatorRepository.Table
                                   select new GetClientPerformanceIndicator
                                   {
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       Date = h.Date,
                                       DueDate = h.DueDate,
                                       Rating = h.Rating,
                                       Remarks = h.Remarks,
                                       ClientId = h.ClientId,
                                       GetClientPerformanceIndicatorTask = (from t in h.ClientPerformanceIndicatorTask
                                                                      select new GetClientPerformanceIndicatorTask
                                                                      {
                                                                          PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                          Title = t.Title,
                                                                          Score = t.Score,
                                                                      }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _ClientPerformanceIndicatorRepository.Table.Any(r => r.Heading.Equals(model.Heading));
            if (isEntityRegistered)
            {
                return BadRequest($"Performance Indicator {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<ClientPerformanceIndicator>(model);
            await _ClientPerformanceIndicatorRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody] PostClientPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _ClientPerformanceIndicatorRepository.Table.Any(r => r.Heading.Equals(model.Heading));
            if (isEntityRegistered)
            {
                return BadRequest($"Performance Indicator {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<ClientPerformanceIndicator>(model);
            await _ClientPerformanceIndicatorRepository.UpdateEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<ClientPerformanceIndicator>(model);
            await _ClientPerformanceIndicatorRepository.UpdateEntity(postEntity);
            return Ok();

        }
        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _ClientPerformanceIndicatorTaskRepository.GetEntity(id);
            await _ClientPerformanceIndicatorTaskRepository.DeleteEntity(entity);
            return Ok();
        }
    }
}
