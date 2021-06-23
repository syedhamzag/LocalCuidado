using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
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
    public class ClientHeartRateController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientHeartRate> _ClientHeartRateRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<HeartRateOfficerToAct> _officertoactRepository;
        private IGenericRepository<HeartRateStaffName> _staffnameRepository;
        private IGenericRepository<HeartRatePhysician> _physicianRepository;

        public ClientHeartRateController(AwesomeCareDbContext dbContext, IGenericRepository<ClientHeartRate> ClientHeartRateRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<HeartRateOfficerToAct> officertoactRepository,
        IGenericRepository<HeartRateStaffName> staffnameRepository, IGenericRepository<HeartRatePhysician> physicianRepository)
        {
            _ClientHeartRateRepository = ClientHeartRateRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientHeartRate
        /// <summary>
        /// Get All ClientHeartRate
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientHeartRate>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientHeartRateRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientHeartRate
        /// </summary>
        /// <param name="postClientHeartRate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientHeartRate postClientHeartRate)
        {
            if (postClientHeartRate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientHeartRate = Mapper.Map<ClientHeartRate>(postClientHeartRate);
            await _ClientHeartRateRepository.InsertEntity(ClientHeartRate);
            return Ok();
        }
        /// <summary>
        /// Update ClientHeartRate
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientHeartRate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientHeartRate = Mapper.Map<ClientHeartRate>(model);
            await _ClientHeartRateRepository.UpdateEntity(ClientHeartRate);
            return Ok();

        }
        /// <summary>
        /// Get ClientHeartRate by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientHeartRate), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientHeartRate = await (from c in _ClientHeartRateRepository.Table
                                           where c.HeartRateId == id
                                           select new GetClientHeartRate
                                           {
                                               HeartRateId = c.HeartRateId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               Age = c.Age,
                                               Date = c.Date,
                                               BeatsPerSeconds = c.BeatsPerSeconds,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comment = c.Comment,
                                               Gender = c.Gender,
                                               GenderAttach = c.GenderAttach,
                                               PhysicianResponse = c.PhysicianResponse,
                                               SeeChart = c.SeeChart,
                                               SeeChartAttach = c.SeeChartAttach,
                                               TargetHR = c.TargetHR,
                                               TargetHRAttach = c.TargetHRAttach,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.HeartRateId == c.HeartRateId
                                                               select new GetHeartRateOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.HeartRateId == c.HeartRateId
                                                            select new GetHeartRateStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.HeartRateId == c.HeartRateId
                                                            select new GetHeartRatePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientHeartRate);
        }
        #endregion
    }
}