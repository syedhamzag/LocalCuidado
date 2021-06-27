using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataTransferObject.DTOs.ClientService;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientServiceWatchController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientServiceWatch> _clientServiceWatchRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<ServiceOfficerToAct> _officertoactRepository;
        private IGenericRepository<ServiceStaffName> _staffnameRepository;

        public ClientServiceWatchController(AwesomeCareDbContext dbContext, IGenericRepository<ClientServiceWatch> clientServiceWatchRepository,
             IGenericRepository<StaffPersonalInfo> staffRepository,
        IGenericRepository<ServiceOfficerToAct> officertoactRepository,
        IGenericRepository<ServiceStaffName> staffnameRepository)
        {
            _clientServiceWatchRepository = clientServiceWatchRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _staffRepository = staffRepository;
        }
        #region ClientServiceWatch
        /// <summary>
        /// Get All ClientServiceWatch
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientServiceWatch>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientServiceWatchRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientServiceWatch
        /// </summary>
        /// <param name="postClientServiceWatch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientServiceWatch postClientServiceWatch)
        {
            if (postClientServiceWatch == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientServiceWatch = Mapper.Map<ClientServiceWatch>(postClientServiceWatch);
            await _clientServiceWatchRepository.InsertEntity(ClientServiceWatch);
            return Ok();
        }
        /// <summary>
        /// Update ClientServiceWatch
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientServiceWatch models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<ServiceOfficerToAct>();
                var filterentity = entity.Where(c => c.ServiceId == model.ServiceId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
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
                var entity = _dbContext.Set<ServiceStaffName>();
                var filterentity = entity.Where(c => c.ServiceId == model.ServiceId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var ClientServiceWatch = Mapper.Map<ClientServiceWatch>(models);
            await _clientServiceWatchRepository.UpdateEntity(ClientServiceWatch);
            return Ok();

        }

        /// <summary>
        /// Get ClientServiceWatch by ServiceWatchId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientServiceWatch), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientServiceWatch = await (from c in _clientServiceWatchRepository.Table
                                           where c.WatchId == id
                                           select new GetClientServiceWatch
                                           {
                                               WatchId = c.WatchId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               URL = c.URL,
                                               Observation = c.Observation,
                                               Incident = c.Incident,
                                               Contact = c.Contact,
                                               Details = c.Details,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.ServiceId == c.WatchId
                                                               select new GetServiceOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.ServiceId == c.WatchId
                                                            select new GetServiceStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientServiceWatch);
        }
        #endregion
    }
}