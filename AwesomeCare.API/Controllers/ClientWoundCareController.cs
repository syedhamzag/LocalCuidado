using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
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
    public class ClientWoundCareController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientWoundCare> _ClientWoundCareRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<WoundCareOfficerToAct> _officertoactRepository;
        private IGenericRepository<WoundCareStaffName> _staffnameRepository;
        private IGenericRepository<WoundCarePhysician> _physicianRepository;

        public ClientWoundCareController(AwesomeCareDbContext dbContext, IGenericRepository<ClientWoundCare> ClientWoundCareRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<WoundCareOfficerToAct> officertoactRepository,
        IGenericRepository<WoundCareStaffName> staffnameRepository, IGenericRepository<WoundCarePhysician> physicianRepository)
        {
            _ClientWoundCareRepository = ClientWoundCareRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientWoundCare
        /// <summary>
        /// Get All ClientWoundCare
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientWoundCare>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientWoundCareRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientWoundCare
        /// </summary>
        /// <param name="postClientWoundCare"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientWoundCare postClientWoundCare)
        {
            if (postClientWoundCare == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientWoundCare = Mapper.Map<ClientWoundCare>(postClientWoundCare);
            await _ClientWoundCareRepository.InsertEntity(ClientWoundCare);
            return Ok();
        }
        /// <summary>
        /// Update ClientWoundCare
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientWoundCare model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientWoundCare = Mapper.Map<ClientWoundCare>(model);
            await _ClientWoundCareRepository.InsertEntity(ClientWoundCare);
            return Ok();

        }
        /// <summary>
        /// Get ClientWoundCare by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientWoundCare), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientWoundCare = await (from c in _ClientWoundCareRepository.Table
                                           where c.WoundCareId == id
                                           select new GetClientWoundCare
                                           {
                                               WoundCareId = c.WoundCareId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               WoundCause = c.WoundCause,
                                               Date = c.Date,
                                               StatusImage = c.StatusImage,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comment = c.Comment,
                                               UlcerStage = c.UlcerStage,
                                               UlcerStageAttach = c.UlcerStageAttach,
                                               Measurment = c.Measurment,
                                               MeasurementAttach = c.MeasurementAttach,
                                               Location = c.Location,
                                               LocationAttach = c.LocationAttach,
                                               Goal = c.Goal,
                                               PainLvl = c.PainLvl,
                                               PhysicianResponse = c.PhysicianResponse,
                                               StatusAttach = c.StatusAttach,
                                               Type = c.Type,
                                               TypeAttach = c.TypeAttach,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.WoundCareId == c.WoundCareId
                                                               select new GetWoundCareOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.WoundCareId == c.WoundCareId
                                                            select new GetWoundCareStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.WoundCareId == c.WoundCareId
                                                            select new GetWoundCarePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientWoundCare);
        }
        #endregion
    }
}