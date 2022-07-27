using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.HealthCondition;
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
    public class HealthConditionController : ControllerBase
    {
        private IGenericRepository<HealthCondition> _HealthConditionRepository;

        public HealthConditionController(IGenericRepository<HealthCondition> HealthConditionRepository)
        {

            _HealthConditionRepository = HealthConditionRepository;
        }
        #region HealthCondition
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHealthCondition>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _HealthConditionRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostHealthCondition post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var HealthCondition = Mapper.Map<HealthCondition>(post);
            await _HealthConditionRepository.InsertEntity(HealthCondition);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutHealthCondition models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var HealthCondition = Mapper.Map<HealthCondition>(models);
            await _HealthConditionRepository.UpdateEntity(HealthCondition);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHealthCondition), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _HealthConditionRepository.Table
                                   where c.HCId == id.Value
                                   select new GetHealthCondition
                                   {
                                       HCId = c.HCId,
                                       Description = c.Description,
                                       Name = c.Name
                                       
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _HealthConditionRepository.GetEntity(id);
            await _HealthConditionRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
