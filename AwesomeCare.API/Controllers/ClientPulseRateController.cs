using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientPulseRateController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientPulseRate> _ClientPulseRateRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<PulseRateOfficerToAct> _officertoactRepository;
        private IGenericRepository<PulseRateStaffName> _staffnameRepository;
        private IGenericRepository<PulseRatePhysician> _physicianRepository;

        public ClientPulseRateController(AwesomeCareDbContext dbContext, IGenericRepository<ClientPulseRate> ClientPulseRateRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<PulseRateOfficerToAct> officertoactRepository,
        IGenericRepository<PulseRateStaffName> staffnameRepository, IGenericRepository<PulseRatePhysician> physicianRepository)
        {
            _ClientPulseRateRepository = ClientPulseRateRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientPulseRate
        /// <summary>
        /// Get All ClientPulseRate
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientPulseRate>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientPulseRateRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientPulseRate
        /// </summary>
        /// <param name="postClientPulseRate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientPulseRate postClientPulseRate)
        {
            if (postClientPulseRate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientPulseRate = Mapper.Map<ClientPulseRate>(postClientPulseRate);
            await _ClientPulseRateRepository.InsertEntity(ClientPulseRate);
            return Ok();
        }
        /// <summary>
        /// Update ClientPulseRate
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientPulseRate models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<PulseRateOfficerToAct>();
                var filterentity = entity.Where(c => c.PulseRateId == model.PulseRateId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.Physician.ToList())
            {
                var entity = _dbContext.Set<PulseRatePhysician>();
                var filterentity = entity.Where(c => c.PulseRateId == model.PulseRateId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.StaffName.ToList())
            {
                var entity = _dbContext.Set<PulseRateStaffName>();
                var filterentity = entity.Where(c => c.PulseRateId == model.PulseRateId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();

            var ClientPulseRate = Mapper.Map<ClientPulseRate>(models);
            await _ClientPulseRateRepository.UpdateEntity(ClientPulseRate);
            return Ok();

        }
        /// <summary>
        /// Get ClientPulseRate by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientPulseRate), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientPulseRate = await (from c in _ClientPulseRateRepository.Table
                                           where c.PulseRateId == id
                                           select new GetClientPulseRate
                                           {
                                               PulseRateId = c.PulseRateId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               CurrentPulse = c.CurrentPulse,
                                               Date = c.Date,
                                               TargetPulse = c.TargetPulse,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comment = c.Comment,
                                               TargetPulseAttach = c.TargetPulseAttach,
                                               SeeChart = c.SeeChart,
                                               SeeChartAttach = c.SeeChartAttach,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Chart = c.Chart,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.PulseRateId == c.PulseRateId
                                                               select new GetPulseRateOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.PulseRateId == c.PulseRateId
                                                            select new GetPulseRateStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.PulseRateId == c.PulseRateId
                                                            select new GetPulseRatePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientPulseRate);
        }
        #endregion
    }
}