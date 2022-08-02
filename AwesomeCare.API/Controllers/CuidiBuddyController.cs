using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CuidiBuddy;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AwesomeCare.DataTransferObject.DTOs.Client;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuidiBuddyController : ControllerBase
    {
        private IGenericRepository<CuidiBuddy> _CuidiBuddyRepository;
        private IGenericRepository<Client> _clientRepository;

        public CuidiBuddyController(IGenericRepository<CuidiBuddy> CuidiBuddyRepository, IGenericRepository<Client> clientRepository)
        {

            _CuidiBuddyRepository = CuidiBuddyRepository;
            _clientRepository = clientRepository;
        }
        
        [HttpGet("GetCuidi")]
        [ProducesResponseType(type: typeof(List<GetClient>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCuidi()
        {
            var getEntities = _clientRepository.Table.Include(s => s.CuidiBuddy).ToList();
            return Ok(getEntities);
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetCuidiBuddy>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _CuidiBuddyRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostCuidiBuddy post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CuidiBuddy = Mapper.Map<CuidiBuddy>(post);
            await _CuidiBuddyRepository.InsertEntity(CuidiBuddy);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutCuidiBuddy models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CuidiBuddy = Mapper.Map<CuidiBuddy>(models);
            await _CuidiBuddyRepository.UpdateEntity(CuidiBuddy);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetCuidiBuddy), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _CuidiBuddyRepository.Table
                                   where c.ClientId == id.Value
                                   select new GetCuidiBuddy
                                   {
                                       Id = id.Value,
                                       CuidiBuddyId = c.CuidiBuddyId,
                                       ClientId = c.ClientId
                                       

                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
