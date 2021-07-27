using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
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
    public class CapacityController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Capacity> _CapacityRepository;
        private IGenericRepository<CapacityIndicator> _indicatorRepository;

        public CapacityController(AwesomeCareDbContext dbContext, IGenericRepository<Capacity> CapacityRepository,
            IGenericRepository<CapacityIndicator> indicatorRepository)
        {
            _CapacityRepository = CapacityRepository;
            _dbContext = dbContext;
            _indicatorRepository = indicatorRepository;
        }
        #region Capacity
        /// <summary>
        /// Get All Capacity
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetCapacity>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _CapacityRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Capacity
        /// </summary>
        /// <param name="postCapacity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostCapacity postCapacity)
        {
            if (postCapacity == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Capacity = Mapper.Map<Capacity>(postCapacity);
            await _CapacityRepository.InsertEntity(Capacity);
            return Ok();
        }
        /// <summary>
        /// Update Capacity
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutCapacity models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Capacity = Mapper.Map<Capacity>(models);
            await _CapacityRepository.UpdateEntity(Capacity);
            return Ok();

        }
        /// <summary>
        /// Get Capacity by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetCapacity), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCapacity = await (from c in _CapacityRepository.Table
                                           where c.CapacityId == id
                                           select new GetCapacity
                                           {
                                               CapacityId = c.CapacityId,
                                               ClientId = c.ClientId,
                                               Implications = c.Implications,
                                               Pointer = c.Pointer,
                                               Indicator = (from com in _indicatorRepository.Table
                                                            where com.CapacityId == c.CapacityId
                                                            select new GetCapacityIndicator
                                                            {
                                                                BaseRecordId = com.BaseRecordId,
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCapacity);
        }
        #endregion
    }
}