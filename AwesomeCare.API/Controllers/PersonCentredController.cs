using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
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
    public class PersonCentredController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<PersonCentred> _PersonCentredRepository;
        private IGenericRepository<PersonCentredFocus> _focusRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRepository;

        public PersonCentredController(AwesomeCareDbContext dbContext, IGenericRepository<PersonCentred> PersonCentredRepository,
            IGenericRepository<PersonCentredFocus> focusRepository, IGenericRepository<BaseRecordItemModel> baseRepository)
        {
            _PersonCentredRepository = PersonCentredRepository;
            _dbContext = dbContext;
            _focusRepository = focusRepository;
            _baseRepository = baseRepository;
        }
        #region PersonCentred
        /// <summary>
        /// Get All PersonCentred
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPersonCentred>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _PersonCentredRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create PersonCentred
        /// </summary>
        /// <param name="postPersonCentred"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPersonCentred postPersonCentred)
        {
            if (postPersonCentred == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PersonCentred = Mapper.Map<PersonCentred>(postPersonCentred);
            await _PersonCentredRepository.InsertEntity(PersonCentred);
            return Ok();
        }
        /// <summary>
        /// Update PersonCentred
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutPersonCentred models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PersonCentred = Mapper.Map<PersonCentred>(models);
            await _PersonCentredRepository.UpdateEntity(PersonCentred);
            return Ok();

        }
        /// <summary>
        /// Get PersonCentred by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPersonCentred), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPersonCentred = await (from c in _PersonCentredRepository.Table
                                           where c.PersonCentredId == id
                                           select new GetPersonCentred
                                           {
                                               PersonCentredId = c.PersonCentredId,
                                               ClientId = c.ClientId,
                                               Class = c.Class,
                                               ExpSupport = c.ExpSupport,
                                               Focus = (from com in _focusRepository.Table
                                                        join b in _baseRepository.Table on com.BaseRecordId equals b.BaseRecordItemId 
                                                        where com.PersonCentredId == c.PersonCentredId
                                                            select new GetPersonCentredFocus
                                                            {
                                                                
                                                                BaseRecordId = com.BaseRecordId,
                                                                ValueName = b.ValueName
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getPersonCentred);
        }
        #endregion
    }
}