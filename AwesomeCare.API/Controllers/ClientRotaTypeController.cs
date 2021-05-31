using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    /// <summary>
    /// Client Rota Types such as AM, LUNCH, TEA, BED, OTHERS etc
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaTypeController : ControllerBase
    {

        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        public ClientRotaTypeController(IGenericRepository<ClientRotaType> clientRotaTypeRepository)
        {
            _clientRotaTypeRepository = clientRotaTypeRepository;
        }


        /// <summary>
        /// Get Client RotaType by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaTypeById")]
        [ProducesResponseType(type: typeof(GetClientRotaType), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientRotaTypeRepository.Table.ProjectTo<GetClientRotaType>().FirstOrDefault(d => d.ClientRotaTypeId == id && !d.Deleted);

            return Ok(getEntity);
        }
        /// <summary>
        /// Create Client RotaType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientRotaType), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientRotaType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isRotaTypeRegistered = _clientRotaTypeRepository.Table.Any(r => r.RotaType.Equals(model.RotaType, StringComparison.InvariantCultureIgnoreCase));
            if (isRotaTypeRegistered)
            {
                return BadRequest($"Rota Type {model.RotaType} already exist");
            }
            var postEntity = Mapper.Map<ClientRotaType>(model);
            var newEntity = await _clientRotaTypeRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientRotaType>(newEntity);

            return CreatedAtRoute("GetClientRotaTypeById", new { id = getEntity.ClientRotaTypeId }, getEntity);
        }

        /// <summary>
        /// Get all Client RotaTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRotaType>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {

            var getEntities = _clientRotaTypeRepository.Table.Where(r => !r.Deleted).ProjectTo<GetClientRotaType>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Update Client RotaType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRotaType), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRotaType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientRotaTypeRepository.GetEntity(model.ClientRotaTypeId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientRotaTypeRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientRotaType>(updateEntity);

            return Ok(getEntity);


        }
    }
}