using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
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
    public class StaffShadowingController : ControllerBase
    {
        private IGenericRepository<StaffShadowing> _StaffShadowingRepository;
        private ILogger<StaffShadowingController> _logger;
        private AwesomeCareDbContext _dbContext;
        public StaffShadowingController(AwesomeCareDbContext dbContext, IGenericRepository<StaffShadowing> StaffShadowingRepository, ILogger<StaffShadowingController> logger)
        {
            _StaffShadowingRepository = StaffShadowingRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffShadowing), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffShadowingRepository.Table
                                   where h.StaffShadowingId == id && !h.Deleted
                                   select new GetStaffShadowing
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffShadowingId = h.StaffShadowingId,
                                       GetStaffShadowingTask = (from t in h.StaffShadowingTask
                                                                     select new GetStaffShadowingTask
                                                                     {
                                                                         StaffShadowingTaskId = t.StaffShadowingTaskId,
                                                                         Answer = t.Answer,
                                                                         Comment = t.Comment,
                                                                         Title = t.Title,
                                                                         Point = t.Point,
                                                                         Score = t.Score,
                                                                     }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet("GetByStaffPersonalInfo/{id}")]
        [ProducesResponseType(type: typeof(List<GetStaffShadowing>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaffPersonalInfo(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffShadowingRepository.Table
                                   where h.StaffPersonalInfoId == id && !h.Deleted
                                   select new GetStaffShadowing
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffShadowingId = h.StaffShadowingId,
                                       GetStaffShadowingTask = (from t in h.StaffShadowingTask
                                                                     select new GetStaffShadowingTask
                                                                     {
                                                                         StaffShadowingTaskId = t.StaffShadowingTaskId,
                                                                         Answer = t.Answer,
                                                                         Comment = t.Comment,
                                                                         Title = t.Title,
                                                                     }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffShadowing>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffShadowingRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffShadowing>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffShadowing model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _StaffShadowingRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.StaffPersonalInfoId.Equals(model.StaffPersonalInfoId));
            if (isEntityRegistered)
            {
                return BadRequest($"Staff Shadowing {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<StaffShadowing>(model);
            await _StaffShadowingRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffShadowing model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var TaskEntity = _dbContext.Set<StaffShadowingTask>();

                var Tasks = TaskEntity.Where(c => c.StaffShadowingId == model.StaffShadowingId).ToList();
                var putEntity = Mapper.Map<StaffShadowing>(model);

                foreach (var task in Tasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetStaffShadowing>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<StaffShadowing>(model);
                await _StaffShadowingRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
