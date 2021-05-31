using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRegulatoryContactController : ControllerBase
    {
        private IGenericRepository<ClientRegulatoryContact> _clientRegContactRepository;
        public ClientRegulatoryContactController(IGenericRepository<ClientRegulatoryContact> clientRegContactRepository)
        {
            _clientRegContactRepository = clientRegContactRepository;
        }

        //[HttpPost]
        //[ProducesResponseType(type: typeof(GetClientRegulatoryContact), statusCode: StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PostRegulatoryContact([FromBody]PostClientRegulatoryContact model)
        //{
        //    if (model == null || !ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
            

        //    var entity = Mapper.Map<ClientRegulatoryContact>(model);
        //    var newEntity = await _clientRegContactRepository.InsertEntity(entity);
        //    var getEntity = Mapper.Map<GetClientRegulatoryContact>(newEntity);
        //    return CreatedAtAction("GetClientRegulatoryContact", new { id = getEntity.ClientRegulatoryContactId }, getEntity);

        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]List<PostClientRegulatoryContact> models)
        {
            if (models == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entities = Mapper.Map<List<ClientRegulatoryContact>>(models);
            await _clientRegContactRepository.InsertEntities(entities);

            return Ok();

        }


        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(GetClientRegulatoryContact), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClientRegulatoryContact(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _clientRegContactRepository.GetEntity(id);
            var getEntity = Mapper.Map<GetClientRegulatoryContact>(entity);
            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRegulatoryContact>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var getEntity =  _clientRegContactRepository.Table.ProjectTo<GetClientRegulatoryContact>(Mapper.Configuration).ToList();
            //var getEntity = Mapper.Map<GetClientRegulatoryContact>(entity);
            return Ok(getEntity);
        }
    }
}