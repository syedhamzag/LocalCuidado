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
            return Ok(getEntities);
        }

        /// <summary>
        /// Create StaffKeyWorkerVoice
        /// </summary>
        /// <param name="postStaffKeyWorkerVoice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffKeyWorkerVoice postStaffKeyWorkerVoice)
        {
            if (postStaffKeyWorkerVoice == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var StaffKeyWorkerVoice = Mapper.Map<StaffKeyWorkerVoice>(postStaffKeyWorkerVoice);
            await _StaffKeyWorkerVoiceRepository.InsertEntity(StaffKeyWorkerVoice);
            return Ok();
        }
        /// <summary>
        /// Update StaffKeyWorkerVoice
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffKeyWorkerVoice model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffKeyWorkerVoice = Mapper.Map<StaffKeyWorkerVoice>(model);
            await _StaffKeyWorkerVoiceRepository.UpdateEntity(StaffKeyWorkerVoice);
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
                                               RiskAssessment = c.RiskAssessment,
                                               ServicesRequiresServices = c.ServicesRequiresServices,
                                               ServicesRequiresTime = c.ServicesRequiresTime,
                                               Deadline = c.Deadline,
                                               Details = c.Details,
                                               HealthAndWellNessChanges = c.HealthAndWellNessChanges,
                                               StaffId = c.StaffId,
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