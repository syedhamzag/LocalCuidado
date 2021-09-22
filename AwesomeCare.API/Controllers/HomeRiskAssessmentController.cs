using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
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

        /// <summary>
        /// Get HomeRiskAssessment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetHomeRiskAssessmentId")]
        [ProducesResponseType(type: typeof(GetHomeRiskAssessment), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _HomeRiskAssessmentRepository.Table.ProjectTo<GetHomeRiskAssessment>().FirstOrDefault(d => d.HomeRiskAssessmentId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get HomeRiskAssessment with Tasks by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHeadingwithTasks/{id}", Name = "GetHomeRiskAssessmentWithTasksById")]
        [ProducesResponseType(type: typeof(GetHomeRiskAssessmentTask), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetHeadingWithTasks(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _HomeRiskAssessmentRepository.Table.ProjectTo<GetHomeRiskAssessmentTask>().FirstOrDefault(d => d.HomeRiskAssessmentId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get HomeRiskAssessments with Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHeadingsWithTasks", Name = "GetHomeRiskAssessmentsWithTasks")]
        [ProducesResponseType(type: typeof(List<GetHomeRiskAssessmentTask>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetHeadingsWithTasks()
        {

            var getEntities = _HomeRiskAssessmentRepository.Table.ProjectTo<GetHomeRiskAssessmentTask>().ToList();

            return Ok(getEntities);
        }


        /// <summary>
        /// Get All HomeRiskAssessment
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHomeRiskAssessment>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _HomeRiskAssessmentRepository.Table.Where(c => !c.Deleted).ProjectTo<GetHomeRiskAssessment>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create HomeRiskAssessment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetHomeRiskAssessment), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PostHomeRiskAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isEntityRegistered = _HomeRiskAssessmentRepository.Table.Any(r => r.Heading.Equals(model.Heading, StringComparison.InvariantCultureIgnoreCase));
            if (isEntityRegistered)
            {
                return BadRequest($"Home Risk Assessment {model.Heading} already exist");
            }
            var postEntity = Mapper.Map<HomeRiskAssessment>(model);
            var newEntity = await _HomeRiskAssessmentRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetHomeRiskAssessment>(newEntity);

            return CreatedAtRoute("GetHomeRiskAssessmentId", new { id = getEntity.HomeRiskAssessmentId }, getEntity);
        }


        /// <summary>
        /// Create Client homeRisk Heading with Tasks
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("PostHeadingwithTasks")]
        //public async Task<IActionResult> PostHeadingWithTasks([FromBody] PostHomeRiskAssessmentTask model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    bool isEntityRegistered = _HomeRiskAssessmentRepository.Table.Any(r => r.Heading.Equals(model.Heading, StringComparison.InvariantCultureIgnoreCase));
        //    if (isEntityRegistered)
        //    {
        //        return BadRequest($"Home Risk Assessment {model.Heading} already exist");
        //    }
        //    var postEntity = Mapper.Map<HomeRiskAssessment>(model);
        //    var newEntity = await _HomeRiskAssessmentRepository.InsertEntity(postEntity);
        //    var getEntity = Mapper.Map<GetHomeRiskAssessment>(newEntity);

        //    return CreatedAtRoute("GetHomeRiskAssessmentId", new { id = getEntity.HomeRiskAssessmentId }, getEntity);
        //}

        /// <summary>
        /// Update HomeRiskAssessment. Can also delete Heading with corresponding Tasks
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetHomeRiskAssessment), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutHomeRiskAssessment model)
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
                var newEntity = await _HomeRiskAssessmentRepository.UpdateEntity(postEntity);
                var getEntity = Mapper.Map<GetHomeRiskAssessment>(newEntity);
                return Ok(getEntity);
            }
        }
    }
}
