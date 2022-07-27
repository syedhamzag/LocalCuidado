using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PerformanceIndicatorController : ControllerBase
    {
        private IGenericRepository<PerformanceIndicator> _PerformanceIndicatorRepository;
        private IGenericRepository<PerformanceIndicatorTask> _PerformanceIndicatorTaskRepository;
        private ILogger<PerformanceIndicatorController> _logger;
        private AwesomeCareDbContext _dbContext;
        public PerformanceIndicatorController(AwesomeCareDbContext dbContext, IGenericRepository<PerformanceIndicator> PerformanceIndicatorRepository, IGenericRepository<PerformanceIndicatorTask> PerformanceIndicatorTaskRepository,ILogger<PerformanceIndicatorController> logger)
        {
            _PerformanceIndicatorRepository = PerformanceIndicatorRepository;
            _logger = logger;
            _dbContext = dbContext;
            _PerformanceIndicatorTaskRepository = PerformanceIndicatorTaskRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPerformanceIndicator), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _PerformanceIndicatorRepository.Table
                                   where h.PerformanceIndicatorId == id && !h.Deleted
                                   select new GetPerformanceIndicator
                                   {
                                       Deleted = h.Deleted,
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       Date = h.Date,
                                       DueDate = h.DueDate,
                                       Rating = h.Rating,
                                       Remarks = h.Remarks,
                                       GetPerformanceIndicatorTask =    (from t in h.PerformanceIndicatorTask
                                                                        where !t.Deleted
                                                                        select new GetPerformanceIndicatorTask
                                                                        {
                                                                            PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                            Title = t.Title,
                                                                            Score = t.Score,
                                                                            Deleted = t.Deleted,
                                                                        }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPerformanceIndicator>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var getEntity = await (from h in _PerformanceIndicatorRepository.Table
                                  where !h.Deleted
                                  select new GetPerformanceIndicator
                                  {
                                      Deleted = h.Deleted,
                                      Heading = h.Heading,
                                      PerformanceIndicatorId = h.PerformanceIndicatorId,
                                      Date = h.Date,
                                      DueDate = h.DueDate,
                                      Rating = h.Rating,
                                      Remarks = h.Remarks,
                                      GetPerformanceIndicatorTask = (from t in h.PerformanceIndicatorTask
                                                                     where !t.Deleted
                                                                     select new GetPerformanceIndicatorTask
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
        public async Task<IActionResult> Create([FromBody] PostPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _PerformanceIndicatorRepository.Table.Any(r => r.Heading.Equals(model.Heading));
            if (isEntityRegistered)
            {
                return BadRequest($"Performance Indicator {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<PerformanceIndicator>(model);
            await _PerformanceIndicatorRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody] PostPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _PerformanceIndicatorRepository.Table.Any(r => r.Heading.Equals(model.Heading));
            if (isEntityRegistered)
            {
                return BadRequest($"Performance Indicator {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<PerformanceIndicator>(model);
            await _PerformanceIndicatorRepository.UpdateEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<PerformanceIndicator>(model);
            await _PerformanceIndicatorRepository.UpdateEntity(postEntity);
            return Ok();

        }
        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _PerformanceIndicatorTaskRepository.GetEntity(id);
            entity.Deleted = true;
            await _PerformanceIndicatorTaskRepository.UpdateEntity(entity);
            return Ok();
        }
    }
}
