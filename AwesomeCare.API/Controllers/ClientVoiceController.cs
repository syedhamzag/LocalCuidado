using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientVoiceController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientVoice> _clientVoiceRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<VoiceOfficerToAct> _officertoactRepository;
        private IGenericRepository<VoiceCallerName> _callernameRepository;
        private IGenericRepository<VoiceGoodStaff> _goodRepository;
        private IGenericRepository<VoicePoorStaff> _poorRepository;

        public ClientVoiceController(AwesomeCareDbContext dbContext, IGenericRepository<ClientVoice> clientVoiceRepository, IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
        IGenericRepository<VoiceOfficerToAct> officertoactRepository,
        IGenericRepository<VoiceCallerName> callernameRepository,
        IGenericRepository<VoiceGoodStaff> goodRepository,
        IGenericRepository<VoicePoorStaff> poorRepository)
        {
            _clientVoiceRepository = clientVoiceRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _callernameRepository = callernameRepository;
            _staffRepository = staffRepository;
            _goodRepository = goodRepository;
            _poorRepository = poorRepository;
        }
        #region ClientVoice
        /// <summary>
        /// Get All ClientVoice
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientVoice>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientVoiceRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientVoice
        /// </summary>
        /// <param name="postClientVoice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientVoice postClientVoice)
        {
            if (postClientVoice == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientVoice = Mapper.Map<ClientVoice>(postClientVoice);
            await _clientVoiceRepository.InsertEntity(ClientVoice);
            return Ok();
        }
        /// <summary>
        /// Update ClientVoice
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientVoice models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<VoiceOfficerToAct>();
                var filterentity = entity.Where(c => c.VoiceId == model.VoiceId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.GoodStaff.ToList())
            {
                var entity = _dbContext.Set<VoiceGoodStaff>();
                var filterentity = entity.Where(c => c.VoiceId == model.VoiceId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.PoorStaff.ToList())
            {
                var entity = _dbContext.Set<VoicePoorStaff>();
                var filterentity = entity.Where(c => c.VoiceId == model.VoiceId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.CallerName.ToList())
            {
                var entity = _dbContext.Set<VoiceCallerName>();
                var filterentity = entity.Where(c => c.VoiceId == model.VoiceId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var ClientVoice = Mapper.Map<ClientVoice>(models);
            await _clientVoiceRepository.UpdateEntity(ClientVoice);
            return Ok();

        }

        /// <summary>
        /// Get ClientVoice by VoiceId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientVoice), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientVoice = await (from c in _clientVoiceRepository.Table
                                           where c.VoiceId == id.Value
                                           select new GetClientVoice
                                           {
                                               VoiceId = c.VoiceId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               URL = c.URL,
                                               OfficeStaffSupport = c.OfficeStaffSupport,                                               
                                               Remarks = c.Remarks,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               ActionRequired = c.ActionRequired,
                                               ActionsTakenByMPCC = c.ActionsTakenByMPCC,
                                               AreasOfImprovements = c.AreasOfImprovements,
                                               HealthGoalLongTerm = c.HealthGoalLongTerm,
                                               HealthGoalShortTerm = c.HealthGoalShortTerm,
                                               InterestedInPrograms = c.InterestedInPrograms,
                                               NextCheckDate = c.NextCheckDate,
                                               RateServiceRecieving = c.RateServiceRecieving,
                                               RateStaffAttending = c.RateStaffAttending,
                                               SomethingSpecial = c.SomethingSpecial,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.VoiceId == c.VoiceId
                                                               select new GetVoiceOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               CallerName = (from cn in _callernameRepository.Table
                                                               join staff in _staffRepository.Table on cn.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where cn.VoiceId == c.VoiceId
                                                               select new GetVoiceCallerName
                                                               {
                                                                   StaffPersonalInfoId = cn.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               GoodStaff = (from g in _goodRepository.Table
                                                               join staff in _staffRepository.Table on g.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where g.VoiceId == c.VoiceId
                                                               select new GetVoiceGoodStaff
                                                               {
                                                                   StaffPersonalInfoId = g.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               PoorStaff = (from p in _poorRepository.Table
                                                               join staff in _staffRepository.Table on p.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where p.VoiceId == c.VoiceId
                                                               select new GetVoicePoorStaff
                                                               {
                                                                   StaffPersonalInfoId = p.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()

                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientVoice);
        }
        #endregion
    }
}
