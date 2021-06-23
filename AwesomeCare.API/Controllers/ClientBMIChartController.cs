using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
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
    public class ClientBMIChartController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientBMIChart> _ClientBMIChartRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<BMIChartOfficerToAct> _officertoactRepository;
        private IGenericRepository<BMIChartStaffName> _staffnameRepository;
        private IGenericRepository<BMIChartPhysician> _physicianRepository;

        public ClientBMIChartController(AwesomeCareDbContext dbContext, IGenericRepository<ClientBMIChart> ClientBMIChartRepository,
             IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<BMIChartOfficerToAct> officertoactRepository,
        IGenericRepository<BMIChartStaffName> staffnameRepository, IGenericRepository<BMIChartPhysician> physicianRepository)
        {
            _ClientBMIChartRepository = ClientBMIChartRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientBMIChart
        /// <summary>
        /// Get All ClientBMIChart
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientBMIChart>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientBMIChartRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientBMIChart
        /// </summary>
        /// <param name="postClientBMIChart"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostClientBMIChart> postClientBMIChart)
        {
            if (postClientBMIChart == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientBMIChart = Mapper.Map<ClientBMIChart>(postClientBMIChart);
            await _ClientBMIChartRepository.InsertEntity(ClientBMIChart);
            return Ok();
        }
        /// <summary>
        /// Update ClientBMIChart
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientBMIChart model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clientBMIChart = Mapper.Map<ClientBMIChart>(model);
            await _ClientBMIChartRepository.UpdateEntity(clientBMIChart);
            return Ok();

        }
        /// <summary>
        /// Get ClientBMIChart by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientBMIChart), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientBMIChart = await (from c in _ClientBMIChartRepository.Table
                                           where c.BMIChartId == id
                                           select new GetClientBMIChart
                                           {
                                               BMIChartId = c.BMIChartId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Date = c.Date,
                                               Time = c.Time,
                                               Height = c.Height,
                                               Weight = c.Weight,
                                               NumberRange = c.NumberRange,
                                               SeeChart = c.SeeChart,
                                               SeeChartAttach = c.SeeChartAttach,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.BMIChartId == c.BMIChartId
                                                               select new GetBMIChartOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BMIChartId == c.BMIChartId
                                                            select new GetBMIChartStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BMIChartId == c.BMIChartId
                                                            select new GetBMIChartPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientBMIChart);
        }
        #endregion
    }
}