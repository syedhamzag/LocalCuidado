using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaDays;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaDaysController : ControllerBase
    {
        private IGenericRepository<ClientRotaDays> _clientRotaDaysRepository;
        public ClientRotaDaysController(IGenericRepository<ClientRotaDays> clientRotaDaysRepository)
        {
            _clientRotaDaysRepository = clientRotaDaysRepository;
        }

        /// <summary>
        /// Get ClientRotaDays by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaDaysById")]
        [ProducesResponseType(type: typeof(GetClientRotaDays), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientRotaDaysRepository.Table.ProjectTo<GetClientRotaDays>().FirstOrDefault(d => d.ClientRotaDaysId == id);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All ClientRota
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRotaDays>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientRotaDaysRepository.Table.ProjectTo<GetClientRotaDays>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientRota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientRotaDays), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientRotaDays model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var postEntity = Mapper.Map<ClientRotaDays>(model);
            var newEntity = await _clientRotaDaysRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientRotaDays>(newEntity);

            return CreatedAtRoute("GetClientRotaDaysById", new { id = getEntity.ClientRotaDaysId }, getEntity);
        }

        /// <summary>
        /// Update ClientRota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRotaDays), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRotaDays model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientRotaDaysRepository.GetEntity(model.ClientRotaId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientRotaDaysRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientRotaDays>(updateEntity);

            return Ok(getEntity);


        }
    }
}