using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
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
    public class BestInterestAssessmentController : ControllerBase
    {
        private IGenericRepository<BestInterestAssessment> _BestInterestAssessmentRepository;
        private ILogger<BestInterestAssessmentController> _logger;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        public BestInterestAssessmentController(AwesomeCareDbContext dbContext, IGenericRepository<BestInterestAssessment> BestInterestAssessmentRepository,
            ILogger<BestInterestAssessmentController> logger, IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository)
        {
            _BestInterestAssessmentRepository = BestInterestAssessmentRepository;
            _logger = logger;
            _dbContext = dbContext;
            _baseRecordRepository = baseRecordRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetBestInterestAssessment), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _BestInterestAssessmentRepository.Table
                                   where h.ClientId == id && !h.Deleted
                                   select new GetBestInterestAssessment
                                   {
                                       Deleted = h.Deleted,
                                       ClientId = h.ClientId,
                                       Date = h.Date,
                                       BestId = h.BestId,
                                       Name = h.Name,
                                       Position = h.Position,
                                       Signature = h.Signature,
                                       GetBelieveTask = (from t in h.BelieveTask
                                                                    select new GetBelieveTask
                                                                    {
                                                                        BelieveTaskId = t.BelieveTaskId,
                                                                        BestId = t.BestId,
                                                                        ReasonableBelieve = t.ReasonableBelieve,
                                                                    }).ToList(),
                                       GetCareIssuesTask = (from t in h.CareIssuesTask
                                                         select new GetCareIssuesTask
                                                         {
                                                             CareIssuesTaskId = t.CareIssuesTaskId,
                                                             BestId = t.BestId,
                                                             Issues = t.Issues,
                                                         }).ToList(),
                                       GetHealthTask = (from t in h.HealthTask
                                                         select new GetHealthTask
                                                         {
                                                             HealthTaskId = t.HealthTaskId,
                                                             BestId = t.BestId,
                                                             HeadingId = t.HeadingId,
                                                             Title = t.Title,
                                                             Answer = t.Answer,
                                                             Remarks = t.Remarks
                                                         }).ToList(),
                                       GetHealthTask2 = (from t in h.HealthTask2
                                                         select new GetHealthTask2
                                                         {
                                                             HealthTask2Id = t.HealthTask2Id,
                                                             BestId = t.BestId,
                                                             Heading2Id = t.Heading2Id,
                                                             Title = t.Title,
                                                             Answer = t.Answer,
                                                             Remark = t.Remark
                                                         }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetBestInterestAssessment>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _BestInterestAssessmentRepository.Table.Where(c => !c.Deleted).ProjectTo<GetBestInterestAssessment>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostBestInterestAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<BestInterestAssessment>(model);
            await _BestInterestAssessmentRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostBestInterestAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                var postEntity = Mapper.Map<BestInterestAssessment>(model);
                await _BestInterestAssessmentRepository.UpdateEntity(postEntity);
                return Ok();
            
        }
    }
}
