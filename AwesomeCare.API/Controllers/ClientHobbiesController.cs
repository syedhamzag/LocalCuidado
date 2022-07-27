using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientHobbies;
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
    public class ClientHobbiesController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientHobbies> _ClientHobbiesRepository;

        public ClientHobbiesController(IGenericRepository<ClientHobbies> ClientHobbiesRepository, AwesomeCareDbContext dbContext)
        {

            _ClientHobbiesRepository = ClientHobbiesRepository;
            _dbContext = dbContext;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientHobbies>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientHobbiesRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] List<PostClientHobbies> post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientHobbies = Mapper.Map<List<ClientHobbies>>(post);
            await _ClientHobbiesRepository.InsertEntities(ClientHobbies);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientHobbies> models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            foreach (var model in models)
            {
                var entity = _dbContext.Set<ClientHobbies>();
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
            var ClientHobbies = Mapper.Map<List<ClientHobbies>>(models);
            await _ClientHobbiesRepository.InsertEntities(ClientHobbies);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientHobbies), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _ClientHobbiesRepository.Table
                                   where c.ClientId == id.Value
                                   select new GetClientHobbies
                                   {
                                       CHId = c.CHId,
                                       HId = c.HId,
                                       ClientId = c.ClientId
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
