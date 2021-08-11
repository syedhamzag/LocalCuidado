using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
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
    public class EquipmentController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Equipment> _EquipmentRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public EquipmentController(AwesomeCareDbContext dbContext, IGenericRepository<Equipment> EquipmentRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository)
        {
            _EquipmentRepository = EquipmentRepository;
            _dbContext = dbContext;
            _staffRepository = staffRepository;
        }
        #region Equipment
        /// <summary>
        /// Get All Equipment
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetEquipment>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _EquipmentRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Equipment
        /// </summary>
        /// <param name="postEquipment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostEquipment postEquipment)
        {
            if (postEquipment == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Equipment = Mapper.Map<Equipment>(postEquipment);
            await _EquipmentRepository.InsertEntity(Equipment);
            return Ok();
        }
        /// <summary>
        /// Update Equipment
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostEquipment models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Equipment = Mapper.Map<Equipment>(models);
            await _EquipmentRepository.UpdateEntity(Equipment);
            return Ok();

        }
        /// <summary>
        /// Get Equipment by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetEquipment), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getEquipment = await (from c in _EquipmentRepository.Table
                                      where c.PersonalDetailId == id.Value
                                      select new GetEquipment
                                           {
                                               EquipmentId = c.EquipmentId,
                                               PersonalDetailId = c.PersonalDetailId,
                                               Location = c.Location,
                                               Type = c.Type,
                                               Name = c.Name,
                                               NextServiceDate = c.NextServiceDate,
                                               ServiceDate = c.ServiceDate,
                                               Attachment = c.Attachment,
                                               PersonToAct = c.PersonToAct,
                                               Status = c.Status
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getEquipment);
        }
        #endregion
    }
}