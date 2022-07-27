using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Hobbies;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        private IGenericRepository<Hobbies> _HobbiesRepository;

        public HobbiesController(IGenericRepository<Hobbies> HobbiesRepository)
        {

            _HobbiesRepository = HobbiesRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHobbies>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _HobbiesRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostHobbies post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var Hobbies = Mapper.Map<Hobbies>(post);
            await _HobbiesRepository.InsertEntity(Hobbies);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutHobbies models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Hobbies = Mapper.Map<Hobbies>(models);
            await _HobbiesRepository.UpdateEntity(Hobbies);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHobbies), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _HobbiesRepository.Table
                                   where c.HId == id.Value
                                   select new GetHobbies
                                   {
                                       HId = c.HId,
                                       Name = c.Name,
                                       Description = c.Description
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _HobbiesRepository.GetEntity(id);
            await _HobbiesRepository.DeleteEntity(entity);
            return Ok();
        }
    }
}
