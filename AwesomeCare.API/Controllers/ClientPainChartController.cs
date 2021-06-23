using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
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
    public class ClientPainChartController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientPainChart> _ClientPainChartRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<PainChartOfficerToAct> _officertoactRepository;
        private IGenericRepository<PainChartStaffName> _staffnameRepository;
        private IGenericRepository<PainChartPhysician> _physicianRepository;

        public ClientPainChartController(AwesomeCareDbContext dbContext, IGenericRepository<ClientPainChart> ClientPainChartRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<PainChartOfficerToAct> officertoactRepository,
        IGenericRepository<PainChartStaffName> staffnameRepository, IGenericRepository<PainChartPhysician> physicianRepository)
        {
            _ClientPainChartRepository = ClientPainChartRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientPainChart
        /// <summary>
        /// Get All ClientPainChart
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientPainChart>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientPainChartRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientPainChart
        /// </summary>
        /// <param name="postClientPainChart"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientPainChart postClientPainChart)
        {
            if (postClientPainChart == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientPainChart = Mapper.Map<ClientPainChart>(postClientPainChart);
            await _ClientPainChartRepository.InsertEntity(ClientPainChart);
            return Ok();
        }
        /// <summary>
        /// Update ClientPainChart
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientPainChart> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientPainChart = Mapper.Map<ClientPainChart>(model);
            await _ClientPainChartRepository.UpdateEntity(ClientPainChart);
            return Ok();

        }
        /// <summary>
        /// Get ClientPainChart by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientPainChart), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientPainChart = await (from c in _ClientPainChartRepository.Table
                                           where c.PainChartId == id
                                           select new GetClientPainChart
                                           {
                                               PainChartId = c.PainChartId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               Location = c.Location,
                                               Date = c.Date,
                                               LocationAttach = c.LocationAttach,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comment = c.Comment,
                                               PainLvl= c.PainLvl,
                                               StatusImage = c.StatusImage,
                                               StatusAttach = c.StatusAttach,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Type = c.Type,
                                               TypeAttach = c.TypeAttach,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.PainChartId == c.PainChartId
                                                               select new GetPainChartOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.PainChartId == c.PainChartId
                                                            select new GetPainChartStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.PainChartId == c.PainChartId
                                                            select new GetPainChartPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientPainChart);
        }
        #endregion
    }
}