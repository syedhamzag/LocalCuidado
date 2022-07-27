using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition;
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
    public class ClientHealthConditionController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientHealthCondition> _ClientHealthConditionRepository;

        public ClientHealthConditionController(IGenericRepository<ClientHealthCondition> ClientHealthConditionRepository,AwesomeCareDbContext dbContext)
        {

            _ClientHealthConditionRepository = ClientHealthConditionRepository;
            _dbContext = dbContext;
        }
        #region ClientHealthCondition
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientHealthCondition>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientHealthConditionRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] List<PostClientHealthCondition> post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ClientHealthCondition = Mapper.Map<List<ClientHealthCondition>>(post);
            await _ClientHealthConditionRepository.InsertEntities(ClientHealthCondition);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientHealthCondition> models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models)
            {
                var entity = _dbContext.Set<ClientHealthCondition>();
                var filterentity = entity.Where(c => c.ClientId == model.ClientId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var ClientHealthCondition = Mapper.Map<List<ClientHealthCondition>>(models);
            await _ClientHealthConditionRepository.InsertEntities(ClientHealthCondition);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientHealthCondition), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _ClientHealthConditionRepository.Table
                                   where c.ClientId == id.Value
                                   select new GetClientHealthCondition
                                   {
                                       CHCId = c.CHCId,
                                       HCId = c.HCId,
                                       ClientId = c.ClientId,
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
        #endregion
    }
}
