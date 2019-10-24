using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientInvolvingPartyBaseController : ControllerBase
    {
        private IGenericRepository<ClientInvolvingPartyItem> _clientInvolvingPartyBaseRepository;
        public ClientInvolvingPartyBaseController(IGenericRepository<ClientInvolvingPartyItem> clientInvolvingPartyBaseRepository)
        {
            _clientInvolvingPartyBaseRepository = clientInvolvingPartyBaseRepository;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GetClientInvolvingPartyItem), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientInvolvingPartyItem model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientInvolvingPartyBase = Mapper.Map<ClientInvolvingPartyItem>(model);
            var newClientInvolvingPartyBase = await _clientInvolvingPartyBaseRepository.InsertEntity(clientInvolvingPartyBase);
            var getClientInvPartyBase = Mapper.Map<GetClientInvolvingPartyItem>(newClientInvolvingPartyBase);
            return CreatedAtAction("Get", new { id = getClientInvPartyBase.ClientInvolvingPartyItemId }, getClientInvPartyBase);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(GetClientInvolvingPartyItem), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _clientInvolvingPartyBaseRepository.GetEntity(id);
            var getClientInvPartyBase = Mapper.Map<GetClientInvolvingPartyItem>(entity);
            return Ok(getClientInvPartyBase);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientInvolvingPartyItem>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {

            var entity = await _clientInvolvingPartyBaseRepository.GetEntities();
            var getClientInvPartyBase = Mapper.Map<List<GetClientInvolvingPartyItem>>(entity);
            return Ok(getClientInvPartyBase);
        }


        [HttpPut()]
        [ProducesResponseType(type: typeof(GetClientInvolvingPartyItem), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientInvolvingPartyItem model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientInvolvingPartyBaseRepository.GetEntity(model.ClientInvolvingPartyItemId);
            if (entity == null)
                return NotFound();
           
            var clientInvolvingPartyBase = Mapper.Map(model, entity);

            var updateClientInvolvingPartyBase = await _clientInvolvingPartyBaseRepository.UpdateEntity(clientInvolvingPartyBase);
            var getClientInvPartyBase = Mapper.Map<GetClientInvolvingPartyItem>(updateClientInvolvingPartyBase);
            return Ok(getClientInvPartyBase);

        }

    }
}