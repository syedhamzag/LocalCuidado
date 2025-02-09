﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
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
    public class StaffMedCompController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffMedComp> _StaffMedCompRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<MedCompOfficerToAct> _officertoactRepository;

        public StaffMedCompController(AwesomeCareDbContext dbContext, IGenericRepository<StaffMedComp> StaffMedCompRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<MedCompOfficerToAct> officertoactRepository)
        {
            _StaffMedCompRepository = StaffMedCompRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
        }
        #region StaffMedComp
        /// <summary>
        /// Get All StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffMedComp>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffMedCompRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffMedComp
        /// </summary>
        /// <param name="postStaffMedComp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffMedComp postStaffMedComp)
        {
            if (postStaffMedComp == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var StaffMedComp = Mapper.Map<StaffMedComp>(postStaffMedComp);
            await _StaffMedCompRepository.InsertEntity(StaffMedComp);
            return Ok();
        }
        /// <summary>
        /// Update StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffMedComp models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<MedCompOfficerToAct>();
                var filterentity = entity.Where(c => c.MedCompId == model.MedCompId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var result = _dbContext.SaveChanges();
            var StaffMedComp = Mapper.Map<StaffMedComp>(models);
            await _StaffMedCompRepository.UpdateEntity(StaffMedComp);
            return Ok();

        }

        /// <summary>
        /// Get StaffMedComp by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffMedComp), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffMedComp = await (from c in _StaffMedCompRepository.Table
                                           where c.MedCompId == id
                                           select new GetStaffMedComp
                                           {
                                               MedCompId = c.MedCompId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               CarePlan = c.CarePlan,
                                               Details = c.Details,
                                               Deadline = c.Deadline,
                                               StaffId = c.StaffId,
                                               RateStaff = c.RateStaff,
                                               ReadingMedicalPrescriptions = c.ReadingMedicalPrescriptions,
                                               UnderstandingofMedication = c.UnderstandingofMedication,
                                               UnderstandingofRights = c.UnderstandingofRights,
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.MedCompId == c.MedCompId
                                                               select new GetMedCompOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffMedComp);
        }
        #endregion
    }
}