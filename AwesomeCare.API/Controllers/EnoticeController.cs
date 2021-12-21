using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Enotice;
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
    public class EnoticeController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Enotice> _EnoticeRepository;
        
        public EnoticeController(AwesomeCareDbContext dbContext, IGenericRepository<Enotice> EnoticeRepository)
        {
            _EnoticeRepository = EnoticeRepository;
            _dbContext = dbContext;
        }
        #region Enotice
        /// <summary>
        /// Get All Enotice
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetEnotice>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _EnoticeRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Enotice
        /// </summary>
        /// <param name="postEnotice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostEnotice postEnotice)
        {
            if (postEnotice == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Enotice = Mapper.Map<Enotice>(postEnotice);
            var newEnotice = await _EnoticeRepository.InsertEntity(Enotice);
            var getEnotice = Mapper.Map<GetEnotice>(newEnotice);
            return Ok(getEnotice);


        }
        /// <summary>
        /// Update Enotice
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetEnotice), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutEnotice model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _EnoticeRepository.GetEntity(model.EnoticeId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _EnoticeRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetEnotice>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get Enotice by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetEnotice), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getEnotice = await (from c in _EnoticeRepository.Table
                                           where c.EnoticeId == id
                                           select new GetEnotice
                                           {
                                               EnoticeId = c.EnoticeId,
                                               Date = c.Date,
                                               PublishTo = c.PublishTo,
                                               Heading = c.Heading,
                                               Note = c.Note,
                                               PublishBy = c.PublishBy,
                                               Image = c.Image,
                                               Video = c.Video,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getEnotice);
        }
        #endregion
    }
}