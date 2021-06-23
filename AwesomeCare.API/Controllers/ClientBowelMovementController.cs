using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
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
    public class ClientBowelMovementController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientBowelMovement> _ClientBowelMovementRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<BowelMovementOfficerToAct> _officertoactRepository;
        private IGenericRepository<BowelMovementStaffName> _staffnameRepository;
        private IGenericRepository<BowelMovementPhysician> _physicianRepository;

        public ClientBowelMovementController(AwesomeCareDbContext dbContext, IGenericRepository<ClientBowelMovement> ClientBowelMovementRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<BowelMovementOfficerToAct> officertoactRepository,
        IGenericRepository<BowelMovementStaffName> staffnameRepository, IGenericRepository<BowelMovementPhysician> physicianRepository)
        {
            _ClientBowelMovementRepository = ClientBowelMovementRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientBowelMovement
        /// <summary>
        /// Get All ClientBowelMovement
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientBowelMovement>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientBowelMovementRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientBowelMovement
        /// </summary>
        /// <param name="postClientBowelMovement"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientBowelMovement postClientBowelMovement)
        {
            if (postClientBowelMovement == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientBowelMovement = Mapper.Map<ClientBowelMovement>(postClientBowelMovement);
            await _ClientBowelMovementRepository.InsertEntity(ClientBowelMovement);
            return Ok();
        }
        /// <summary>
        /// Update ClientBowelMovement
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientBowelMovement model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientBowelMovement = Mapper.Map<ClientBowelMovement>(model);
            await _ClientBowelMovementRepository.UpdateEntity(ClientBowelMovement);
            return Ok();

        }
        /// <summary>
        /// Get ClientBowelMovement by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientBowelMovement), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientBowelMovement = await (from c in _ClientBowelMovementRepository.Table
                                           where c.BowelMovementId == id
                                           select new GetClientBowelMovement
                                           {
                                               BowelMovementId = c.BowelMovementId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               Size = c.Size,
                                               Date = c.Date,
                                               Type = c.Type,
                                               TypeAttach = c.TypeAttach,
                                               Color = c.Color,
                                               ColorAttach = c.ColorAttach,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               StatusImage = c.StatusImage,
                                               StatusAttach = c.StatusAttach,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.BowelMovementId == c.BowelMovementId
                                                               select new GetBowelMovementOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BowelMovementId == c.BowelMovementId
                                                            select new GetBowelMovementStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.BowelMovementId == c.BowelMovementId
                                                            select new GetBowelMovementPhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientBowelMovement);
        }
        #endregion
    }
}