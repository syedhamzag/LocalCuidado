using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
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
    public class ClientBloodCoagulationRecordController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientBloodCoagulationRecord> _ClientBloodCoagulationRecordRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<BloodCoagOfficerToAct> _officertoactRepository;
        private IGenericRepository<BloodCoagStaffName> _staffnameRepository;
        private IGenericRepository<BloodCoagPhysician> _physicianRepository;

        public ClientBloodCoagulationRecordController(AwesomeCareDbContext dbContext, IGenericRepository<ClientBloodCoagulationRecord> ClientBloodCoagulationRecordRepository,
                    IGenericRepository<BloodCoagOfficerToAct> officertoactRepository,
        IGenericRepository<BloodCoagStaffName> staffnameRepository, IGenericRepository<StaffPersonalInfo> staffRepository,
        IGenericRepository<BloodCoagPhysician> physicianRepository)
        {
            _ClientBloodCoagulationRecordRepository = ClientBloodCoagulationRecordRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientBloodCoagulationRecord
        /// <summary>
        /// Get All ClientBloodCoagulationRecord
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientBloodCoagulationRecord>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientBloodCoagulationRecordRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientBloodCoagulationRecord
        /// </summary>
        /// <param name="postClientBloodCoagulationRecord"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientBloodCoagulationRecord postClientBloodCoagulationRecord)
        {
            if (postClientBloodCoagulationRecord == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientBloodCoagulationRecord = Mapper.Map<ClientBloodCoagulationRecord>(postClientBloodCoagulationRecord);
            await _ClientBloodCoagulationRecordRepository.InsertEntity(ClientBloodCoagulationRecord);
            return Ok();
        }
        /// <summary>
        /// Update ClientBloodCoagulationRecord
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientBloodCoagulationRecord models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<BloodCoagOfficerToAct>();
                var filterentity = entity.Where(c => c.BloodRecordId == model.BloodRecordId).ToList();
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
                var entity = _dbContext.Set<BloodCoagPhysician>();
                var filterentity = entity.Where(c => c.BloodRecordId == model.BloodRecordId).ToList();
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
                var entity = _dbContext.Set<BloodCoagStaffName>();
                var filterentity = entity.Where(c => c.BloodRecordId == model.BloodRecordId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            
            var ClientBloodCoagulationRecord = Mapper.Map<ClientBloodCoagulationRecord>(models);         
            await _ClientBloodCoagulationRecordRepository.UpdateEntity(ClientBloodCoagulationRecord);
            return Ok();
        }
        /// <summary>
        /// Get ClientBloodCoagulationRecord by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientBloodCoagulationRecord), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientBloodCoagulationRecord = await (from c in _ClientBloodCoagulationRecordRepository.Table
                                           where c.BloodRecordId == id
                                           select new GetClientBloodCoagulationRecord
                                           {
                                               BloodRecordId = c.BloodRecordId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               BloodStatus = c.BloodStatus,
                                               CurrentDose = c.CurrentDose,
                                               Date = c.Date,
                                               Indication = c.Indication,
                                               NewDose = c.NewDose,                                               
                                               INR = c.INR,
                                               NewINR = c.NewINR,
                                               PhysicianResponce = c.PhysicianResponce,                                               
                                               TargetINRAttach = c.TargetINRAttach,
                                               TargetINR = c.TargetINR,
                                               Comment = c.Comment,
                                               StartDate = c.StartDate,
                                               Status = c.Status,
                                               Remark = c.Remark,
                                               Time = c.Time,
                                               Deadline = c.Deadline,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.BloodRecordId == c.BloodRecordId
                                                               select new GetBloodCoagOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BloodRecordId == c.BloodRecordId
                                                            select new GetBloodCoagStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BloodRecordId == c.BloodRecordId
                                                            select new GetBloodCoagPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientBloodCoagulationRecord);
        }
        #endregion
    }
}