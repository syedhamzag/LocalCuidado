using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
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
    public class ClientBodyTempController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientBodyTemp> _ClientBodyTempRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<BodyTempOfficerToAct> _officertoactRepository;
        private IGenericRepository<BodyTempStaffName> _staffnameRepository;
        private IGenericRepository<BodyTempPhysician> _physicianRepository;

        public ClientBodyTempController(AwesomeCareDbContext dbContext, IGenericRepository<ClientBodyTemp> ClientBodyTempRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<BodyTempOfficerToAct> officertoactRepository,
        IGenericRepository<BodyTempStaffName> staffnameRepository, IGenericRepository<BodyTempPhysician> physicianRepository)
        {
            _ClientBodyTempRepository = ClientBodyTempRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientBodyTemp
        /// <summary>
        /// Get All ClientBodyTemp
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientBodyTemp>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientBodyTempRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientBodyTemp
        /// </summary>
        /// <param name="postClientBodyTemp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientBodyTemp postClientBodyTemp)
        {
            if (postClientBodyTemp == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientBodyTemp = Mapper.Map<ClientBodyTemp>(postClientBodyTemp);
            await _ClientBodyTempRepository.InsertEntity(ClientBodyTemp);
            return Ok();
        }
        /// <summary>
        /// Update ClientBodyTemp
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientBodyTemp models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<BodyTempOfficerToAct>();
                var filterentity = entity.Where(c => c.BodyTempId == model.BodyTempId).ToList();
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
                var entity = _dbContext.Set<BodyTempPhysician>();
                var filterentity = entity.Where(c => c.BodyTempId == model.BodyTempId).ToList();
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
                var entity = _dbContext.Set<BodyTempStaffName>();
                var filterentity = entity.Where(c => c.BodyTempId == model.BodyTempId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();

            var ClientBodyTemp = Mapper.Map<ClientBodyTemp>(models);
            await _ClientBodyTempRepository.UpdateEntity(ClientBodyTemp);
            return Ok();

        }
        /// <summary>
        /// Get ClientBodyTemp by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientBodyTemp), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientBodyTemp = await (from c in _ClientBodyTempRepository.Table
                                           where c.BodyTempId == id
                                           select new GetClientBodyTemp
                                           {
                                               BodyTempId = c.BodyTempId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Date = c.Date,
                                               Time = c.Time,
                                               TargetTemp = c.TargetTemp,
                                               TargetTempAttach = c.TargetTempAttach,
                                               CurrentReading = c.CurrentReading,
                                               SeeChart = c.SeeChart,
                                               SeeChartAttach = c.SeeChartAttach,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.BodyTempId == c.BodyTempId
                                                               select new GetBodyTempOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BodyTempId == c.BodyTempId
                                                            select new GetBodyTempStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BodyTempId == c.BodyTempId
                                                            select new GetBodyTempPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientBodyTemp);
        }
        #endregion
    }
}