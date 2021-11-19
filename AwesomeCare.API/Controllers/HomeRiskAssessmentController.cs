using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
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
    public class HomeRiskAssessmentController : ControllerBase
    {

        private IGenericRepository<HomeRiskAssessment> _HomeRiskAssessmentRepository;
        private ILogger<HomeRiskAssessmentController> _logger;
        private AwesomeCareDbContext _dbContext;
        public HomeRiskAssessmentController(AwesomeCareDbContext dbContext, IGenericRepository<HomeRiskAssessment> HomeRiskAssessmentRepository, ILogger<HomeRiskAssessmentController> logger)
        {
            _HomeRiskAssessmentRepository = HomeRiskAssessmentRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHomeRiskAssessment), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _HomeRiskAssessmentRepository.Table
                             where h.HomeRiskAssessmentId == id && !h.Deleted
                             select new GetHomeRiskAssessment
                             {
                                 Deleted = h.Deleted,
                                 ClientId = h.ClientId,
                                 Heading = h.Heading,
                                 HomeRiskAssessmentId = h.HomeRiskAssessmentId,
                                 GetHomeRiskAssessmentTask = (from t in h.HomeRiskAssessmentTask
                                                             select new GetHomeRiskAssessmentTask
                                                             {
                                                                HomeRiskAssessmentTaskId = t.HomeRiskAssessmentTaskId,
                                                                Answer = t.Answer,
                                                                Comment = t.Comment,
                                                                Title = t.Title,
                                                             }).ToList()
                             }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        [HttpGet("GetByClient/{id}")]
        [ProducesResponseType(type: typeof(List<GetHomeRiskAssessment>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByClient(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _HomeRiskAssessmentRepository.Table
                                   where h.ClientId == id && !h.Deleted
                                   select new GetHomeRiskAssessment
                                   {
                                       Deleted = h.Deleted,
                                       ClientId = h.ClientId,
                                       Heading = h.Heading,
                                       HomeRiskAssessmentId = h.HomeRiskAssessmentId,
                                       GetHomeRiskAssessmentTask = (from t in h.HomeRiskAssessmentTask
                                                                    select new GetHomeRiskAssessmentTask
                                                                    {
                                                                        HomeRiskAssessmentTaskId = t.HomeRiskAssessmentTaskId,
                                                                        Answer = t.Answer,
                                                                        Comment = t.Comment,
                                                                        Title = t.Title,
                                                                    }).ToList()
                                   }).ToListAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHomeRiskAssessment>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _HomeRiskAssessmentRepository.Table.Where(c => !c.Deleted).ProjectTo<GetHomeRiskAssessment>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostHomeRiskAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEntityRegistered = _HomeRiskAssessmentRepository.Table.Any(r => r.Heading.Equals(model.Heading) && r.ClientId.Equals(model.ClientId));
            if (isEntityRegistered)
            {
                return BadRequest($"Home Risk Assessment {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<HomeRiskAssessment>(model);
             await _HomeRiskAssessmentRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostHomeRiskAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Deleted)
            {
                //get all corresponding tasks and mark as deleted
                var homeRiskTaskEntity = _dbContext.Set<HomeRiskAssessmentTask>();

                var homeRiskTasks = homeRiskTaskEntity.Where(c => c.HomeRiskAssessmentId == model.HomeRiskAssessmentId).ToList();
                var putEntity = Mapper.Map<HomeRiskAssessment>(model);

                foreach (var task in homeRiskTasks)
                {
                    task.Deleted = true;
                    _dbContext.Entry(task).State = EntityState.Modified;
                }
                _dbContext.Entry(putEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                var getEntity = Mapper.Map<GetHomeRiskAssessment>(putEntity);
                return Ok(getEntity);
            }
            else
            {
                var postEntity = Mapper.Map<HomeRiskAssessment>(model);
                await _HomeRiskAssessmentRepository.UpdateEntity(postEntity);
                return Ok();
            }
        }
    }
}
