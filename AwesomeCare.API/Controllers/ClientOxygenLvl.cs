using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
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
    public class ClientOxygenLvlController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientOxygenLvl> _ClientOxygenLvlRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<OxygenLvlOfficerToAct> _officertoactRepository;
        private IGenericRepository<OxygenLvlStaffName> _staffnameRepository;
        private IGenericRepository<OxygenLvlPhysician> _physicianRepository;

        public ClientOxygenLvlController(AwesomeCareDbContext dbContext, IGenericRepository<ClientOxygenLvl> ClientOxygenLvlRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<OxygenLvlOfficerToAct> officertoactRepository,
        IGenericRepository<OxygenLvlStaffName> staffnameRepository, IGenericRepository<OxygenLvlPhysician> physicianRepository)
        {
            _ClientOxygenLvlRepository = ClientOxygenLvlRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientOxygenLvl
        /// <summary>
        /// Get All ClientOxygenLvl
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientOxygenLvl>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientOxygenLvlRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientOxygenLvl
        /// </summary>
        /// <param name="postClientOxygenLvl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientOxygenLvl postClientOxygenLvl)
        {
            if (postClientOxygenLvl == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientOxygenLvl = Mapper.Map<ClientOxygenLvl>(postClientOxygenLvl);
            await _ClientOxygenLvlRepository.InsertEntity(ClientOxygenLvl);
            return Ok();
        }
        /// <summary>
        /// Update ClientOxygenLvl
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientOxygenLvl> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientOxygenLvl = Mapper.Map<ClientOxygenLvl>(model);
            await _ClientOxygenLvlRepository.UpdateEntity(ClientOxygenLvl);
            return Ok();

        }
        /// <summary>
        /// Get ClientOxygenLvl by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientOxygenLvl), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientOxygenLvl = await (from c in _ClientOxygenLvlRepository.Table
                                           where c.OxygenLvlId == id
                                           select new GetClientOxygenLvl
                                           {
                                               OxygenLvlId = c.OxygenLvlId,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               CurrentReading = c.CurrentReading,
                                               Date = c.Date,
                                               SeeChart = c.SeeChart,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               SeeChartAttach = c.SeeChartAttach,
                                               TargetOxygen = c.TargetOxygen,
                                               TargetOxygenAttach = c.TargetOxygenAttach,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.OxygenLvlId == c.OxygenLvlId
                                                               select new GetOxygenLvlOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.OxygenLvlId == c.OxygenLvlId
                                                            select new GetOxygenLvlStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.OxygenLvlId == c.OxygenLvlId
                                                            select new GetOxygenLvlPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()

                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientOxygenLvl);
        }
        #endregion
    }
}