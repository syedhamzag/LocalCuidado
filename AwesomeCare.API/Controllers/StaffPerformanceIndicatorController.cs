using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using AwesomeCare.Model.Models;
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
    public class StaffPerformanceIndicatorController : ControllerBase
    {
        private IGenericRepository<PerformanceIndicator> _PerformanceIndicatorRepository;
        private ILogger<StaffPerformanceIndicatorController> _logger;
        private AwesomeCareDbContext _dbContext;
        public StaffPerformanceIndicatorController(AwesomeCareDbContext dbContext, IGenericRepository<PerformanceIndicator> PerformanceIndicatorRepository, ILogger<StaffPerformanceIndicatorController> logger)
        {
            _PerformanceIndicatorRepository = PerformanceIndicatorRepository;
            _logger = logger;
            _dbContext = dbContext;
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
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       GetPerformanceIndicatorTask = (from t in h.PerformanceIndicatorTask
                                                                select new GetPerformanceIndicatorTask
                                                                {
                                                                    PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                    Title = t.Title,
                                                                    Score = t.Score,
                                                                }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet("GetByStaffPersonalInfo/{id}")]
        [ProducesResponseType(type: typeof(List<GetPerformanceIndicator>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaffPersonalInfo(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _PerformanceIndicatorRepository.Table
                                   where h.StaffPersonalInfoId == id && !h.Deleted
                                   select new GetPerformanceIndicator
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       PerformanceIndicatorId = h.PerformanceIndicatorId,
                                       GetPerformanceIndicatorTask = (from t in h.PerformanceIndicatorTask
                                                                select new GetPerformanceIndicatorTask
                                                                {
                                                                    PerformanceIndicatorTaskId = t.PerformanceIndicatorTaskId,
                                                                    Title = t.Title,
                                                                    Score = t.Score
                                                                }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPerformanceIndicator>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _PerformanceIndicatorRepository.Table.Where(c => !c.Deleted).ProjectTo<GetPerformanceIndicator>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _PerformanceIndicatorRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.StaffPersonalInfoId.Equals(model.StaffPersonalInfoId));
            if (isEntityRegistered)
            {
                return BadRequest($"Staff Performance Indicator {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<PerformanceIndicator>(model);
            await _PerformanceIndicatorRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostPerformanceIndicator model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var TaskEntity = _dbContext.Set<PerformanceIndicatorTask>();

                var Tasks = TaskEntity.Where(c => c.PerformanceIndicatorId == model.PerformanceIndicatorId).ToList();
                var putEntity = Mapper.Map<PerformanceIndicator>(model);

                foreach (var task in Tasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetPerformanceIndicator>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<PerformanceIndicator>(model);
                await _PerformanceIndicatorRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
