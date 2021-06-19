using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Resources;
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
    public class ResourcesController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Resources> _ResourcesRepository;
        
        public ResourcesController(AwesomeCareDbContext dbContext, IGenericRepository<Resources> ResourcesRepository)
        {
            _ResourcesRepository = ResourcesRepository;
            _dbContext = dbContext;
        }
        #region Resources
        /// <summary>
        /// Get All Resources
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetResources>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ResourcesRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Resources
        /// </summary>
        /// <param name="postResources"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostResources postResources)
        {
            if (postResources == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Resources = Mapper.Map<Resources>(postResources);
            var newResources = await _ResourcesRepository.InsertEntity(Resources);
            var getResources = Mapper.Map<GetResources>(newResources);
            return Ok(getResources);


        }
        /// <summary>
        /// Update Resources
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetResources), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutResources model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _ResourcesRepository.GetEntity(model.ResourcesId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _ResourcesRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetResources>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get Resources by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetResources), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getResources = await (from c in _ResourcesRepository.Table
                                           where c.ResourcesId == id
                                           select new GetResources
                                           {
                                               ResourcesId = c.ResourcesId,
                                               Date = c.Date,
                                               PublishTo = c.PublishTo,
                                               Heading = c.Heading,
                                               Note = c.Note,
                                               PublishBy = c.PublishBy,
                                               Image = c.Image,
                                               Video = c.Video,
                                               
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getResources);
        }
        #endregion
    }
}