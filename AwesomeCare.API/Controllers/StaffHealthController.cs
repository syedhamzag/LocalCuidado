using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
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
    public class StaffHealthController : ControllerBase
    {
        private IGenericRepository<StaffHealth> _StaffHealthRepository;
        private ILogger<StaffHealthController> _logger;
        private AwesomeCareDbContext _dbContext;
        public StaffHealthController(AwesomeCareDbContext dbContext, IGenericRepository<StaffHealth> StaffHealthRepository, ILogger<StaffHealthController> logger)
        {
            _StaffHealthRepository = StaffHealthRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffHealth), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffHealthRepository.Table
                                   where h.StaffHealthId == id && !h.Deleted
                                   select new GetStaffHealth
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffHealthId = h.StaffHealthId,
                                       GetStaffHealthTask = (from t in h.StaffHealthTask
                                                                     select new GetStaffHealthTask
                                                                     {
                                                                         StaffHealthTaskId = t.StaffHealthTaskId,
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
        [ProducesResponseType(type: typeof(List<GetStaffHealth>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaffPersonalInfo(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffHealthRepository.Table
                                   where h.StaffPersonalInfoId == id && !h.Deleted
                                   select new GetStaffHealth
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffHealthId = h.StaffHealthId,
                                       GetStaffHealthTask = (from t in h.StaffHealthTask
                                                                     select new GetStaffHealthTask
                                                                     {
                                                                         StaffHealthTaskId = t.StaffHealthTaskId,
                                                                         Answer = t.Answer,
                                                                         Comment = t.Comment,
                                                                         Title = t.Title,
                                                                     }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffHealth>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffHealthRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffHealth>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffHealth model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _StaffHealthRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.StaffPersonalInfoId.Equals(model.StaffPersonalInfoId));
            if (isEntityRegistered)
            {
                return BadRequest($"Staff Health {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<StaffHealth>(model);
            await _StaffHealthRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffHealth model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var TaskEntity = _dbContext.Set<StaffHealthTask>();

                var Tasks = TaskEntity.Where(c => c.StaffHealthId == model.StaffHealthId).ToList();
                var putEntity = Mapper.Map<StaffHealth>(model);

                foreach (var task in Tasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetStaffHealth>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<StaffHealth>(model);
                await _StaffHealthRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
