using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskBoardController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<TaskBoard> _TaskBoardRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<RotaPin> _rotaPinRepository;

        public TaskBoardController(AwesomeCareDbContext dbContext, IGenericRepository<TaskBoard> TaskBoardRepository, IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<RotaPin> rotaPinRepository)
        {
            _TaskBoardRepository = TaskBoardRepository;
            _staffRepository = staffRepository;
            _dbContext = dbContext;
            _rotaPinRepository = rotaPinRepository;
        }
        #region TaskBoard

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetTaskBoard>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _TaskBoardRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpGet("GetWithStaff/")]
        [ProducesResponseType(type: typeof(List<GetTaskBoard>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWithStaff()
        {
            var getTaskBoard = (from c in _TaskBoardRepository.Table
                                      select new GetTaskBoard
                                      {
                                          TaskId = c.TaskId,
                                          AssignedBy = c.AssignedBy,
                                          Attachment = c.Attachment,
                                          CompletionDate = c.CompletionDate,
                                          Note = c.Note,
                                          Status = c.Status,
                                          TaskImage = c.TaskImage,
                                          TaskName = c.TaskName,
                                          AssignedTo = (from com in c.AssignedTo
                                                        join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                        where com.TaskBoardId == c.TaskId
                                                        select new GetTaskBoardAssignedTo
                                                        {
                                                            StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                            TaskBoardAssignedToId = com.TaskBoardAssignedToId,
                                                            TaskBoardId = com.TaskBoardId,
                                                            StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                        }).ToList()
                                      }
                      ).ToList();
            return Ok(getTaskBoard);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostTaskBoard post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var TaskBoard = Mapper.Map<TaskBoard>(post);
            await _TaskBoardRepository.InsertEntity(TaskBoard);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutTaskBoard models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.AssignedTo.ToList())
            {
                var entity = _dbContext.Set<TaskBoardAssignedTo>();
                var filterentity = entity.Where(c => c.TaskBoardId == model.TaskBoardId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var TaskBoard = Mapper.Map<TaskBoard>(models);
            await _TaskBoardRepository.UpdateEntity(TaskBoard);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetTaskBoard), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getTaskBoard = await (from c in _TaskBoardRepository.Table
                                 where c.TaskId == id.Value
                                 select new GetTaskBoard
                                 {
                                     TaskId = c.TaskId,
                                     AssignedBy = c.AssignedBy,
                                     Attachment = c.Attachment,
                                     CompletionDate = c.CompletionDate,
                                     Note = c.Note,
                                     Status = c.Status,
                                     TaskImage = c.TaskImage,
                                     TaskName = c.TaskName,
                                     AssignedTo = (from com in c.AssignedTo
                                                   join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                   where com.TaskBoardId == c.TaskId
                                                   select new GetTaskBoardAssignedTo
                                                   {
                                                       StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                       TaskBoardAssignedToId = com.TaskBoardAssignedToId,
                                                       TaskBoardId = com.TaskBoardId,
                                                       StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                   }).ToList()
                                 }
                      ).FirstOrDefaultAsync();
            return Ok(getTaskBoard);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromBody] List<GetTaskBoard> model)
        {
            var entity = _dbContext.Set<TaskBoard>();
            foreach (var item in model)
            {
                var filterentity = entity.Where(c => c.TaskId == item.TaskId).FirstOrDefault();
                if (filterentity != null)
                {
                    filterentity.Status = 16;
                    _dbContext.Entry(filterentity).State = EntityState.Modified;

                }
            }
            _dbContext.SaveChanges();
            await _TaskBoardRepository.GetEntities();
            return Ok();

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangePin([FromBody] PostRotaPin model)
        {
            var postEntity = Mapper.Map<RotaPin>(model);
            await _rotaPinRepository.UpdateEntity(postEntity);
            return Ok();
        }

        [HttpGet("GetPin")]
        [ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPin()
        {
            var getEntity = _rotaPinRepository.Table.Where(s => s.PinId == 2).ProjectTo<GetRotaPin>().FirstOrDefault();

            return Ok(getEntity);
        }
        #endregion
    }
}
