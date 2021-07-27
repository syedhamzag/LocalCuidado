using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
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
    public class PersonalController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Personal> _PersonalRepository;

        public PersonalController(AwesomeCareDbContext dbContext, IGenericRepository<Personal> PersonalRepository)
        {
            _PersonalRepository = PersonalRepository;
            _dbContext = dbContext;
        }
        #region Personal
        /// <summary>
        /// Get All Personal
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPersonal>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _PersonalRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Personal
        /// </summary>
        /// <param name="postPersonal"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPersonal postPersonal)
        {
            if (postPersonal == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Personal = Mapper.Map<Personal>(postPersonal);
            await _PersonalRepository.InsertEntity(Personal);
            return Ok();
        }
        /// <summary>
        /// Update Personal
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutPersonal models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Personal = Mapper.Map<Personal>(models);
            await _PersonalRepository.UpdateEntity(Personal);
            return Ok();

        }
        /// <summary>
        /// Get Personal by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPersonal), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPersonal = await (from c in _PersonalRepository.Table
                                           where c.PersonalId == id
                                           select new GetPersonal
                                           {
                                               PersonalId = c.PersonalId,
                                               ClientId = c.ClientId,
                                               Smoking = c.Smoking,
                                               DNR = c.DNR
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getPersonal);
        }
        #endregion
    }
}