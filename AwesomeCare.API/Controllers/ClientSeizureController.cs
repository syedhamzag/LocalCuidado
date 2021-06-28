using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
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
    public class ClientSeizureController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientSeizure> _ClientSeizureRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<SeizureOfficerToAct> _officertoactRepository;
        private IGenericRepository<SeizureStaffName> _staffnameRepository;
        private IGenericRepository<SeizurePhysician> _physicianRepository;

        public ClientSeizureController(AwesomeCareDbContext dbContext, IGenericRepository<ClientSeizure> ClientSeizureRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<SeizureOfficerToAct> officertoactRepository,
        IGenericRepository<SeizureStaffName> staffnameRepository, IGenericRepository<SeizurePhysician> physicianRepository)
        {
            _ClientSeizureRepository = ClientSeizureRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientSeizure
        /// <summary>
        /// Get All ClientSeizure
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientSeizure>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientSeizureRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientSeizure
        /// </summary>
        /// <param name="postClientSeizure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientSeizure postClientSeizure)
        {
            if (postClientSeizure == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientSeizure = Mapper.Map<ClientSeizure>(postClientSeizure);
            await _ClientSeizureRepository.InsertEntity(ClientSeizure);
            return Ok();
        }
        /// <summary>
        /// Update ClientSeizure
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientSeizure models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<SeizureOfficerToAct>();
                var filterentity = entity.Where(c => c.SeizureId == model.SeizureId).ToList();
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
                var entity = _dbContext.Set<SeizurePhysician>();
                var filterentity = entity.Where(c => c.SeizureId == model.SeizureId).ToList();
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
                var entity = _dbContext.Set<SeizureStaffName>();
                var filterentity = entity.Where(c => c.SeizureId == model.SeizureId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var ClientSeizure = Mapper.Map<ClientSeizure>(models);
            await _ClientSeizureRepository.UpdateEntity(ClientSeizure);
            return Ok();

        }
        /// <summary>
        /// Get ClientSeizure by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientSeizure), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientSeizure = await (from c in _ClientSeizureRepository.Table
                                           where c.SeizureId == id
                                           select new GetClientSeizure
                                           {
                                               SeizureId = c.SeizureId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               Often = c.Often,
                                               Date = c.Date,
                                               WhatHappened = c.WhatHappened,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               SeizureLength = c.SeizureLength,
                                               SeizureLengthAttach = c.SeizureLengthAttach,
                                               SeizureType = c.SeizureType,
                                               PhysicianResponse = c.PhysicianResponse,
                                               StatusAttach = c.StatusAttach,
                                               SeizureTypeAttach = c.SeizureTypeAttach,
                                               StatusImage = c.StatusImage,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.SeizureId == c.SeizureId
                                                               select new GetSeizureOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.SeizureId == c.SeizureId
                                                            select new GetSeizureStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.SeizureId == c.SeizureId
                                                            select new GetSeizurePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientSeizure);
        }
        #endregion
    }
}