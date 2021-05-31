using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffWorkTeamController : ControllerBase
    {
        private IGenericRepository<StaffWorkTeam> _staffWorkTeamRepository;
        private ILogger<StaffWorkTeamController> _logger;

        public StaffWorkTeamController(IGenericRepository<StaffWorkTeam> staffWorkTeamRepository, ILogger<StaffWorkTeamController> logger)
        {
            _staffWorkTeamRepository = staffWorkTeamRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get WorkTeam by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetStaffWorkTeamId")]
        [ProducesResponseType(type: typeof(GetStaffWorkTeam), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _staffWorkTeamRepository.Table.ProjectTo<GetStaffWorkTeam>().FirstOrDefault(d => d.StaffWorkTeamId == id);

            return Ok(getEntity);
        }
        /// <summary>
        /// Create RotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetStaffWorkTeam), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PostStaffWorkTeam model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isworkTeamRegistered = _staffWorkTeamRepository.Table.Any(r => r.WorkTeam.ToLower() == model.WorkTeam.ToLower());
            if (isworkTeamRegistered)
            {
                return BadRequest($"WorkTeam {model.WorkTeam} already exist");
            }
            var postEntity = Mapper.Map<StaffWorkTeam>(model);
            var newEntity = await _staffWorkTeamRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetStaffWorkTeam>(newEntity);

            return CreatedAtRoute("GetStaffWorkTeamId", new { id = getEntity.StaffWorkTeamId }, getEntity);
        }

        /// <summary>
        /// Get all RotaTasks
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffWorkTeam>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _staffWorkTeamRepository.Table.ProjectTo<GetStaffWorkTeam>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Update RotaTask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetStaffWorkTeam), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffWorkTeam model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _staffWorkTeamRepository.GetEntity(model.StaffWorkTeamId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _staffWorkTeamRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetStaffWorkTeam>(updateEntity);

            return Ok(getEntity);


        }
    }
}