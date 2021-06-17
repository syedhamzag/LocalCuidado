using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorkerVoice;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffKeyWorkerVoiceController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffKeyWorkerVoice> _StaffKeyWorkerVoiceRepository;
        
        public StaffKeyWorkerVoiceController(AwesomeCareDbContext dbContext, IGenericRepository<StaffKeyWorkerVoice> StaffKeyWorkerVoiceRepository)
        {
            _StaffKeyWorkerVoiceRepository = StaffKeyWorkerVoiceRepository;
            _dbContext = dbContext;
        }
        #region StaffKeyWorkerVoice
        /// <summary>
        /// Get All StaffKeyWorkerVoice
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffKeyWorkerVoice>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffKeyWorkerVoiceRepository.Table.ToList();
            return Ok(getEntities.Distinct().ToList());
        }


        /// <summary>
        /// Get All StaffKeyWorkerVoice
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffKeyWorkerVoice>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffKeyWorkerVoiceRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffKeyWorkerVoice
        /// </summary>
        /// <param name="postStaffKeyWorkerVoice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffKeyWorkerVoice> postStaffKeyWorkerVoice)
        {
            if (postStaffKeyWorkerVoice == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffKeyWorkerVoice)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var StaffKeyWorkerVoice = Mapper.Map<List<StaffKeyWorkerVoice>>(postStaffKeyWorkerVoice);
            await _StaffKeyWorkerVoiceRepository.InsertEntities(StaffKeyWorkerVoice);
            return Ok();
        }
        /// <summary>
        /// Update StaffKeyWorkerVoice
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffKeyWorkerVoice> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffKeyWorkerVoice>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffKeyWorkerVoice item in filterEntity)
            {
                var modelRecord = model.Select(s => s).Where(s => s.OfficertoAct == item.OfficerToAct).FirstOrDefault();
                if (modelRecord == null)
                {
                    _dbContext.Entry(item).State = EntityState.Deleted;

                }
                else
                {
                    var putEntity = Mapper.Map(modelRecord, item);
                    _dbContext.Entry(putEntity).State = EntityState.Modified;
                }

            }
            //Model not in Database
            foreach (var item in model)
            {
                var NotInDb = filterEntity.FirstOrDefault(r => r.OfficerToAct == item.OfficertoAct);
                if (NotInDb == null)
                {
                    var postEntity = Mapper.Map<StaffKeyWorkerVoice>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }

        /// <summary>
        /// Get StaffKeyWorkerVoice by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffKeyWorkerVoice), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffKeyWorkerVoice = await (from c in _StaffKeyWorkerVoiceRepository.Table
                                           where c.KeyWorkerId == id
                                           select new GetStaffKeyWorkerVoice
                                           {
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               MedicationChanges = c.MedicationChanges,
                                               MovingAndHandling = c.MovingAndHandling,
                                               NotComfortableServices = c.NotComfortableServices,
                                               NutritionalChanges = c.NutritionalChanges,
                                               OfficertoAct = c.OfficerToAct,
                                               RiskAssessment = c.RiskAssessment,
                                               ServicesRequiresServices = c.ServicesRequiresServices,
                                               ServicesRequiresTime = c.ServicesRequiresTime,
                                               Deadline = c.Deadline,
                                               Details = c.Details,
                                               HealthAndWellNessChanges = c.HealthAndWellNessChanges,
                                               StaffId = c.StaffId,
                                               TeamYouWorkFor = c.TeamYouWorkFor,
                                               WellSupportedServices = c.WellSupportedServices,
                                               ChangesWeNeed = c.ChangesWeNeed,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffKeyWorkerVoice);
        }
        #endregion
    }
}