using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLead;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffTeamLeadController : ControllerBase
    {
        private IGenericRepository<StaffTeamLead> _StaffTeamLeadRepository;
        private ILogger<StaffTeamLeadController> _logger;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        public StaffTeamLeadController(AwesomeCareDbContext dbContext, IGenericRepository<StaffTeamLead> StaffTeamLeadRepository,
            ILogger<StaffTeamLeadController> logger, IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository)
        {
            _StaffTeamLeadRepository = StaffTeamLeadRepository;
            _logger = logger;
            _dbContext = dbContext;
            _baseRecordRepository = baseRecordRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffTeamLead), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffTeamLeadRepository.Table
                                   where h.TeamLeadId == id && !h.Deleted
                                   select new GetStaffTeamLead
                                   {
                                       Deleted = h.Deleted,
                                       ClientInvolved = h.ClientInvolved,
                                       StaffInvolved = h.StaffInvolved,
                                       Date = h.Date,
                                       DidYouDo = h.DidYouDo,
                                       DidYouObserved = h.DidYouObserved,
                                       OfficeToDo = h.OfficeToDo,
                                       Rota = h.Rota,
                                       StaffStoppedWorking = h.StaffStoppedWorking,
                                       GetStaffTeamLeadTasks = (from t in h.StaffTeamLeadTasks
                                                                    select new GetStaffTeamLeadTasks
                                                                    {
                                                                        TeamLeadTaskId = t.TeamLeadTaskId,
                                                                        Status = t.Status,
                                                                        Comments = t.Comments,
                                                                        Title = t.Title,
                                                                    }).ToList()
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffTeamLead>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffTeamLeadRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffTeamLead>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffTeamLead model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffTeamLead>(model);
            await _StaffTeamLeadRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffTeamLead model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get all corresponding tasks and mark as deleted
            var teamLeadTaskEntity = _dbContext.Set<StaffTeamLeadTasks>();

            var teamLeadTasks = teamLeadTaskEntity.Where(c => c.TeamLeadId == model.TeamLeadId).ToList();
            var putEntity = Mapper.Map<StaffTeamLead>(model);

            foreach (var task in teamLeadTasks)
            {
                task.Deleted = true;
                _dbContext.Entry(task).State = EntityState.Modified;
         
            }
            _dbContext.Entry(putEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            var getEntity = Mapper.Map<GetStaffTeamLead>(putEntity);
            return Ok(getEntity);
        }
    }
}
