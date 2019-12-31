using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaNameController : ControllerBase
    {
        private IGenericRepository<Rota> _rotaRepository;
        public ClientRotaNameController(IGenericRepository<Rota> rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        /// <summary>
        /// Get Rota by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaNameById")]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _rotaRepository.Table.ProjectTo<GetClientRotaName>().FirstOrDefault(d => d.RotaId == id && !d.Deleted);

            return Ok(getEntity);
        }
        /// <summary>
        /// Create Rota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientRotaName model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isRotaNameRegistered = _rotaRepository.Table.Any(r => r.RotaName.Equals(model.RotaName, StringComparison.InvariantCultureIgnoreCase));
            if (isRotaNameRegistered)
            {
                return BadRequest($"Rota name {model.RotaName} already exist");
            }
            var postRota = Mapper.Map<Rota>(model);
            var newRota = await _rotaRepository.InsertEntity(postRota);
            var getEntity = Mapper.Map<GetClientRotaName>(newRota);

            return CreatedAtRoute("GetClientRotaNameById", new { id = getEntity.RotaId }, getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRotaName>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {

            var getEntities = _rotaRepository.Table.Where(r=>!r.Deleted).ProjectTo<GetClientRotaName>().ToList();
            return Ok(getEntities);
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRotaName model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity =await _rotaRepository.GetEntity(model.RotaId);
            var putRota = Mapper.Map(model, entity);
            var updateEntity =await _rotaRepository.UpdateEntity(putRota);
            var getEntity = Mapper.Map<GetClientRotaName>(updateEntity);

            return Ok(getEntity);
           

        }
    }
}