using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
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
    public class StaffAdlObsController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffAdlObs> _StaffAdlObsRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<AdlObsOfficerToAct> _officertoactRepository;

        public StaffAdlObsController(AwesomeCareDbContext dbContext, IGenericRepository<StaffAdlObs> StaffAdlObsRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<AdlObsOfficerToAct> officertoactRepository)
        {
            _StaffAdlObsRepository = StaffAdlObsRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
        }
        #region StaffAdlObs
        /// <summary>
        /// Get All StaffAdlObs
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffAdlObs>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffAdlObsRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create StaffAdlObs
        /// </summary>
        /// <param name="postStaffAdlObs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffAdlObs postStaffAdlObs)
        {
            if (postStaffAdlObs == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var StaffAdlObs = Mapper.Map<StaffAdlObs>(postStaffAdlObs);
            await _StaffAdlObsRepository.InsertEntity(StaffAdlObs);
            return Ok();
        }
        /// <summary>
        /// Update StaffAdlObs
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffAdlObs models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<AdlObsOfficerToAct>();
                var filterentity = entity.Where(c => c.ObservationId == model.ObservationId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var result = _dbContext.SaveChanges();
            var StaffAdlObs = Mapper.Map<StaffAdlObs>(models);
            await _StaffAdlObsRepository.UpdateEntity(StaffAdlObs);
            return Ok();

        }


        /// <summary>
        /// Get StaffAdlObs by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffAdlObs), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffAdlObs = await (from c in _StaffAdlObsRepository.Table
                                           where c.ObservationID == id
                                           select new GetStaffAdlObs
                                           {
                                               ObservationID = c.ObservationID,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comments = c.Comments,
                                               Details = c.Details,
                                               FivePrinciples = c.FivePrinciples,
                                               StaffId = c.StaffId,
                                               UnderstandingofControl = c.UnderstandingofControl,
                                               UnderstandingofEquipment = c.UnderstandingofEquipment,
                                               UnderstandingofService = c.UnderstandingofService,
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.ObservationId == c.ObservationID
                                                               select new GetAdlObsOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffAdlObs);
        }
        #endregion
    }
}