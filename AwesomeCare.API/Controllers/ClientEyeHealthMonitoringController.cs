using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
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
    public class ClientEyeHealthMonitoringController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientEyeHealthMonitoring> _ClientEyeHealthMonitoringRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<EyeHealthOfficerToAct> _officertoactRepository;
        private IGenericRepository<EyeHealthStaffName> _staffnameRepository;
        private IGenericRepository<EyeHealthPhysician> _physicianRepository;

        public ClientEyeHealthMonitoringController(AwesomeCareDbContext dbContext, IGenericRepository<ClientEyeHealthMonitoring> ClientEyeHealthMonitoringRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<EyeHealthOfficerToAct> officertoactRepository,
        IGenericRepository<EyeHealthStaffName> staffnameRepository, IGenericRepository<EyeHealthPhysician> physicianRepository)
        {
            _ClientEyeHealthMonitoringRepository = ClientEyeHealthMonitoringRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientEyeHealthMonitoring
        /// <summary>
        /// Get All ClientEyeHealthMonitoring
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientEyeHealthMonitoring>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientEyeHealthMonitoringRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientEyeHealthMonitoring
        /// </summary>
        /// <param name="postClientEyeHealthMonitoring"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientEyeHealthMonitoring postClientEyeHealthMonitoring)
        {
            if (postClientEyeHealthMonitoring == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientEyeHealthMonitoring = Mapper.Map<ClientEyeHealthMonitoring>(postClientEyeHealthMonitoring);
            await _ClientEyeHealthMonitoringRepository.InsertEntity(ClientEyeHealthMonitoring);
            return Ok();
        }
        /// <summary>
        /// Update ClientEyeHealthMonitoring
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientEyeHealthMonitoring models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<EyeHealthOfficerToAct>();
                var filterentity = entity.Where(c => c.EyeHealthId == model.EyeHealthId).ToList();
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
                var entity = _dbContext.Set<EyeHealthPhysician>();
                var filterentity = entity.Where(c => c.EyeHealthId == model.EyeHealthId).ToList();
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
                var entity = _dbContext.Set<EyeHealthStaffName>();
                var filterentity = entity.Where(c => c.EyeHealthId == model.EyeHealthId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();

            var ClientEyeHealthMonitoring = Mapper.Map<ClientEyeHealthMonitoring>(models);
            await _ClientEyeHealthMonitoringRepository.UpdateEntity(ClientEyeHealthMonitoring);
            return Ok();

        }
        /// <summary>
        /// Get ClientEyeHealthMonitoring by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientEyeHealthMonitoring), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientEyeHealthMonitoring = await (from c in _ClientEyeHealthMonitoringRepository.Table
                                           where c.EyeHealthId == id
                                           select new GetClientEyeHealthMonitoring
                                           {
                                               EyeHealthId = c.EyeHealthId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               CurrentScore = c.CurrentScore,
                                               Date = c.Date,
                                               MethodUsed = c.MethodUsed,
                                               MethodUsedAttach = c.MethodUsedAttach,
                                               ToolUsed = c.ToolUsed,
                                               ToolUsedAttach = c.ToolUsedAttach,
                                               PatientGlasses = c.PatientGlasses,
                                               TargetSet = c.TargetSet,
                                               StatusImage = c.StatusImage,
                                               StatusAttach = c.StatusAttach,
                                               Comment = c.Comment,
                                               Deadline = c.Deadline,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.EyeHealthId == c.EyeHealthId
                                                               select new GetEyeHealthOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.EyeHealthId == c.EyeHealthId
                                                            select new GetEyeHealthStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.EyeHealthId == c.EyeHealthId
                                                            select new GetEyeHealthPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientEyeHealthMonitoring);
        }
        #endregion
    }
}