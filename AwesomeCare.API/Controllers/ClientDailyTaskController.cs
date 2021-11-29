using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;
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
    public class ClientDailyTaskController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientDailyTask> _ClientDailyTaskRepository;

        public ClientDailyTaskController(AwesomeCareDbContext dbContext, IGenericRepository<ClientDailyTask> ClientDailyTaskRepository)
        {

            _dbContext = dbContext;
            _ClientDailyTaskRepository = ClientDailyTaskRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientDailyTask>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientDailyTaskRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientDailyTask post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ClientDailyTask = Mapper.Map<ClientDailyTask>(post);
            await _ClientDailyTaskRepository.InsertEntity(ClientDailyTask);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientDailyTask models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientDailyTask = Mapper.Map<ClientDailyTask>(models);
            await _ClientDailyTaskRepository.UpdateEntity(ClientDailyTask);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientDailyTask), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _ClientDailyTaskRepository.Table
                                   where c.DailyTaskId == id.Value
                                   select new GetClientDailyTask
                                   {
                                       DailyTaskId = c.DailyTaskId,
                                       Date = c.Date,
                                       AmendmentDate = c.AmendmentDate,
                                       DailyTaskName = c.DailyTaskName,
                                       ClientId = c.ClientId,
                                       Attachment = c.Attachment,
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
