using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.IncomingMeds;
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
    public class IncomingMedsController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<IncomingMeds> _IncomingMedsRepository;
        
        public IncomingMedsController(AwesomeCareDbContext dbContext, IGenericRepository<IncomingMeds> IncomingMedsRepository)
        {
            _IncomingMedsRepository = IncomingMedsRepository;
            _dbContext = dbContext;
        }
        #region IncomingMeds
        /// <summary>
        /// Get All IncomingMeds
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetIncomingMeds>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _IncomingMedsRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create IncomingMeds
        /// </summary>
        /// <param name="postIncomingMeds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostIncomingMeds postIncomingMeds)
        {
            if (postIncomingMeds == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IncomingMeds = Mapper.Map<IncomingMeds>(postIncomingMeds);
            var newIncomingMeds = await _IncomingMedsRepository.InsertEntity(IncomingMeds);
            var getIncomingMeds = Mapper.Map<GetIncomingMeds>(newIncomingMeds);
            return Ok(getIncomingMeds);


        }
        /// <summary>
        /// Update IncomingMeds
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetIncomingMeds), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutIncomingMeds model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _IncomingMedsRepository.GetEntity(model.IncomingMedsId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _IncomingMedsRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetIncomingMeds>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get IncomingMeds by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetIncomingMeds), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getIncomingMeds = await (from c in _IncomingMedsRepository.Table
                                           where c.IncomingMedsId == id
                                           select new GetIncomingMeds
                                           {
                                               IncomingMedsId = c.IncomingMedsId,
                                               Date = c.Date,
                                               UserName = c.UserName,
                                               StaffName = c.StaffName,
                                               StartDate = c.StartDate,
                                               ChartImage = c.ChartImage,
                                               MedsImage = c.MedsImage,
                                               Status = c.Status,
                                               
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getIncomingMeds);
        }
        #endregion
    }
}