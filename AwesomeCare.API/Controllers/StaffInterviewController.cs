using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
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
    public class StaffInterviewController : ControllerBase
    {
        private IGenericRepository<StaffInterview> _StaffInterviewRepository;
        private ILogger<StaffInterviewController> _logger;
        private AwesomeCareDbContext _dbContext;
        public StaffInterviewController(AwesomeCareDbContext dbContext, IGenericRepository<StaffInterview> StaffInterviewRepository, ILogger<StaffInterviewController> logger)
        {
            _StaffInterviewRepository = StaffInterviewRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffInterview), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffInterviewRepository.Table
                                   where h.StaffInterviewId == id && !h.Deleted
                                   select new GetStaffInterview
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffInterviewId = h.StaffInterviewId,
                                       GetStaffInterviewTask = (from t in h.StaffInterviewTask
                                                                     select new GetStaffInterviewTask
                                                                     {
                                                                         StaffInterviewTaskId = t.StaffInterviewTaskId,
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
        [ProducesResponseType(type: typeof(List<GetStaffInterview>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaffPersonalInfo(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffInterviewRepository.Table
                                   where h.StaffPersonalInfoId == id && !h.Deleted
                                   select new GetStaffInterview
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffInterviewId = h.StaffInterviewId,
                                       GetStaffInterviewTask = (from t in h.StaffInterviewTask
                                                                     select new GetStaffInterviewTask
                                                                     {
                                                                         StaffInterviewTaskId = t.StaffInterviewTaskId,
                                                                         Answer = t.Answer,
                                                                         Comment = t.Comment,
                                                                         Title = t.Title,
                                                                     }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffInterview>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffInterviewRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffInterview>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffInterview model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _StaffInterviewRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.StaffPersonalInfoId.Equals(model.StaffPersonalInfoId));
            if (isEntityRegistered)
            {
                return BadRequest($"Staff Interview {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<StaffInterview>(model);
            await _StaffInterviewRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffInterview model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var TaskEntity = _dbContext.Set<StaffInterviewTask>();

                var Tasks = TaskEntity.Where(c => c.StaffInterviewId == model.StaffInterviewId).ToList();
                var putEntity = Mapper.Map<StaffInterview>(model);

                foreach (var task in Tasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetStaffInterview>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<StaffInterview>(model);
                await _StaffInterviewRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
