using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaController : ControllerBase
    {
        private IGenericRepository<ClientRota> _clientRotaRepository;
        public ClientRotaController(IGenericRepository<ClientRota> clientRotaRepository)
        {
            _clientRotaRepository = clientRotaRepository;
        }

        /// <summary>
        /// Get ClientRota by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaById")]
        [ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientRotaRepository.Table.ProjectTo<GetClientRota>().FirstOrDefault(d => d.ClientRotaId == id);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All ClientRota
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRota>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientRotaRepository.Table.ProjectTo<GetClientRota>().ToList();
            return Ok(getEntities);
        }

        ///// <summary>
        ///// Create ClientRota
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost()]
        //[Route("[action]")]
        //[ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post([FromBody]PostClientRota model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var postEntity = Mapper.Map<ClientRota>(model);
        //    var newEntity = await _clientRotaRepository.InsertEntity(postEntity);
        //    var getEntity = Mapper.Map<GetClientRota>(newEntity);

        //    return CreatedAtRoute("GetClientRotaById", new { id = getEntity.ClientRotaId }, getEntity);
        //}

        /// <summary>
        /// Update ClientRota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRota model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientRotaRepository.GetEntity(model.ClientRotaId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientRotaRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientRota>(updateEntity);

            return Ok(getEntity);


        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateRota([FromBody]List<CreateClientRota> model)
        {

            var postEntity = Mapper.Map<List<ClientRota>>(model);
            await _clientRotaRepository.InsertEntities(postEntity);
            return Ok();
        }
    }
}