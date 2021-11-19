using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
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
    public class StaffCompetenceTestController : ControllerBase
    {
        private IGenericRepository<StaffCompetenceTest> _StaffCompetenceTestRepository;
        private ILogger<StaffCompetenceTestController> _logger;
        private AwesomeCareDbContext _dbContext;
        public StaffCompetenceTestController(AwesomeCareDbContext dbContext, IGenericRepository<StaffCompetenceTest> StaffCompetenceTestRepository, ILogger<StaffCompetenceTestController> logger)
        {
            _StaffCompetenceTestRepository = StaffCompetenceTestRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffCompetenceTest), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffCompetenceTestRepository.Table
                                   where h.StaffCompetenceTestId == id && !h.Deleted
                                   select new GetStaffCompetenceTest
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffCompetenceTestId = h.StaffCompetenceTestId,
                                       GetStaffCompetenceTestTask = (from t in h.StaffCompetenceTestTask
                                                                    select new GetStaffCompetenceTestTask
                                                                    {
                                                                        StaffCompetenceTestTaskId = t.StaffCompetenceTestTaskId,
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
        [ProducesResponseType(type: typeof(List<GetStaffCompetenceTest>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaffPersonalInfo(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffCompetenceTestRepository.Table
                                   where h.StaffPersonalInfoId == id && !h.Deleted
                                   select new GetStaffCompetenceTest
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Heading = h.Heading,
                                       StaffCompetenceTestId = h.StaffCompetenceTestId,
                                       GetStaffCompetenceTestTask = (from t in h.StaffCompetenceTestTask
                                                                    select new GetStaffCompetenceTestTask
                                                                    {
                                                                        StaffCompetenceTestTaskId = t.StaffCompetenceTestTaskId,
                                                                        Answer = t.Answer,
                                                                        Comment = t.Comment,
                                                                        Title = t.Title,
                                                                    }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffCompetenceTest>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffCompetenceTestRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffCompetenceTest>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffCompetenceTest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _StaffCompetenceTestRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.StaffPersonalInfoId.Equals(model.StaffPersonalInfoId));
            if (isEntityRegistered)
            {
                return BadRequest($"Staff Competence Test {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<StaffCompetenceTest>(model);
            await _StaffCompetenceTestRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffCompetenceTest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var TaskEntity = _dbContext.Set<StaffCompetenceTestTask>();

                var Tasks = TaskEntity.Where(c => c.StaffCompetenceTestId == model.StaffCompetenceTestId).ToList();
                var putEntity = Mapper.Map<StaffCompetenceTest>(model);

                foreach (var task in Tasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetStaffCompetenceTest>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<StaffCompetenceTest>(model);
                await _StaffCompetenceTestRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
