using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientInvolvingPartyController : ControllerBase
    {
        private IGenericRepository<ClientInvolvingParty> _clientInvolvingPartyRepository;
        public ClientInvolvingPartyController(IGenericRepository<ClientInvolvingParty> clientInvolvingPartyRepository)
        {
            _clientInvolvingPartyRepository = clientInvolvingPartyRepository;
        }

        //[HttpPost]
        //[ProducesResponseType(type: typeof(GetClientInvolvingParty), statusCode: StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post([FromBody]PostClientInvolvingParty model)
        //{
        //    if (model == null || !ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var clientInvolvingParty = Mapper.Map<ClientInvolvingParty>(model);
        //    var newClientInvolvingPartyBase = await _clientInvolvingPartyRepository.InsertEntity(clientInvolvingParty);
        //    var getClientInvPartyBase = Mapper.Map<GetClientInvolvingParty>(newClientInvolvingPartyBase);
        //    return CreatedAtAction("Get", new { id = getClientInvPartyBase.ClientInvolvingPartyId }, getClientInvPartyBase);

        //}

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]List<PostClientInvolvingParty> models)
        {
            if (models == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientInvolvingParty = Mapper.Map<List<ClientInvolvingParty>>(models);
            await _clientInvolvingPartyRepository.InsertEntities(clientInvolvingParty);

            return Ok();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(GetClientInvolvingParty), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _clientInvolvingPartyRepository.GetEntity(id);
            var getClientInvParty = Mapper.Map<GetClientInvolvingParty>(entity);
            return Ok(getClientInvParty);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientInvolvingParty>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {

            var entity = await _clientInvolvingPartyRepository.GetEntities();
            var getClientInvParty = Mapper.Map<List<GetClientInvolvingParty>>(entity);
            return Ok(getClientInvParty);
        }

        [HttpPut()]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientInvolvingParty> model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in model)
            {
                var entity = await _clientInvolvingPartyRepository.GetEntity(item.ClientInvolvingPartyId);
                if (entity == null)
                {
                    var createInvolvingParty = Mapper.Map(item, entity);
                    await _clientInvolvingPartyRepository.InsertEntity(createInvolvingParty);

                }
                else
                {
                    var clientInvolvingParty = Mapper.Map(item, entity);
                    await _clientInvolvingPartyRepository.UpdateEntity(clientInvolvingParty);
                }                
            }        
            return Ok();

        }
    }
}