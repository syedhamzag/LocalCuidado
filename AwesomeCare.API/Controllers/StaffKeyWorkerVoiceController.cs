using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffKeyWorkerVoiceController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffKeyWorkerVoice> _StaffKeyWorkerVoiceRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<KeyWorkerOfficerToAct> _officertoactRepository;
        private IGenericRepository<KeyWorkerWorkteam> _workteamRepository;

        public StaffKeyWorkerVoiceController(AwesomeCareDbContext dbContext, IGenericRepository<StaffKeyWorkerVoice> StaffKeyWorkerVoiceRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<KeyWorkerOfficerToAct> officertoactRepository, IGenericRepository<KeyWorkerWorkteam> workteamRepository)
        {
            _StaffKeyWorkerVoiceRepository = StaffKeyWorkerVoiceRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
            _workteamRepository = workteamRepository;
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
        public async Task<IActionResult> Put([FromBody] PutStaffKeyWorkerVoice models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<KeyWorkerOfficerToAct>();
                var filterentity = entity.Where(c => c.KeyWorkerId == model.KeyWorkerId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.Workteam.ToList())
            {
                var entity = _dbContext.Set<KeyWorkerWorkteam>();
                var filterentity = entity.Where(c => c.KeyWorkerId == model.KeyWorkerId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var StaffKeyWorkerVoice = Mapper.Map<StaffKeyWorkerVoice>(models);
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
                                               KeyWorkerId = c.KeyWorkerId,
                                               Reference = c.Reference,
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
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.KeyWorkerId == c.KeyWorkerId
                                                               select new GetKeyWorkerOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               Workteam = (from com in _workteamRepository.Table
                                                           join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                           where com.KeyWorkerId == c.KeyWorkerId
                                                           select new GetKeyWorkerWorkteam
                                                           {
                                                               StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                               StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                           }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffKeyWorkerVoice);
        }
        #endregion
    }
}