using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
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
    public class ClientBloodPressureController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientBloodPressure> _ClientBloodPressureRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<BloodPressureOfficerToAct> _officertoactRepository;
        private IGenericRepository<BloodPressureStaffName> _staffnameRepository;
        private IGenericRepository<BloodPressurePhysician> _physicianRepository;

        public ClientBloodPressureController(AwesomeCareDbContext dbContext, IGenericRepository<ClientBloodPressure> ClientBloodPressureRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<BloodPressureOfficerToAct> officertoactRepository,
        IGenericRepository<BloodPressureStaffName> staffnameRepository, IGenericRepository<BloodPressurePhysician> physicianRepository)
        {
            _ClientBloodPressureRepository = ClientBloodPressureRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientBloodPressure
        /// <summary>
        /// Get All ClientBloodPressure
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientBloodPressure>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientBloodPressureRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientBloodPressure
        /// </summary>
        /// <param name="postClientBloodPressure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientBloodPressure postClientBloodPressure)
        {
            if (postClientBloodPressure == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientBloodPressure = Mapper.Map<ClientBloodPressure>(postClientBloodPressure);
            await _ClientBloodPressureRepository.InsertEntity(ClientBloodPressure);
            return Ok();
        }
        /// <summary>
        /// Update ClientBloodPressure
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientBloodPressure models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<BloodPressureOfficerToAct>();
                var filterentity = entity.Where(c => c.BloodPressureId == model.BloodPressureId).ToList();
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
                var entity = _dbContext.Set<BloodPressurePhysician>();
                var filterentity = entity.Where(c => c.BloodPressureId == model.BloodPressureId).ToList();
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
                var entity = _dbContext.Set<BloodPressureStaffName>();
                var filterentity = entity.Where(c => c.BloodPressureId == model.BloodPressureId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();

            var clientBloodPressure = Mapper.Map<ClientBloodPressure>(models);
            await _ClientBloodPressureRepository.UpdateEntity(clientBloodPressure);
            return Ok();

        }
        /// <summary>
        /// Get ClientBloodPressure by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientBloodPressure), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientBloodPressure = await (from c in _ClientBloodPressureRepository.Table
                                           where c.BloodPressureId == id
                                           select new GetClientBloodPressure
                                           {
                                               BloodPressureId = c.BloodPressureId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Date = c.Date,
                                               Time = c.Time,
                                               GoalSystolic = c.GoalSystolic,
                                               GoalDiastolic = c.GoalDiastolic,
                                               ReadingSystolic = c.ReadingSystolic,
                                               ReadingDiastolic = c.ReadingDiastolic,
                                               StatusImage = c.StatusImage,
                                               StatusAttach = c.StatusAttach,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.BloodPressureId == c.BloodPressureId
                                                               select new GetBloodPressureOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BloodPressureId == c.BloodPressureId
                                                            select new GetBloodPressureStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BloodPressureId == c.BloodPressureId
                                                            select new GetBloodPressurePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientBloodPressure);
        }
        #endregion
    }
}