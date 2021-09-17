using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.Model.Models;
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
        private IGenericRepository<TaskBoard> _TaskBoardRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public TaskBoardController(IGenericRepository<TaskBoard> TaskBoardRepository, IGenericRepository<StaffPersonalInfo> staffRepository)
        {
            _TaskBoardRepository = TaskBoardRepository;
            _staffRepository = staffRepository;
        }
        #region TaskBoard
        /// <summary>
        /// Get All TaskBoard
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetTaskBoard>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _TaskBoardRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create TaskBoard
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Update TaskBoard
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostTaskBoard models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var TaskBoard = Mapper.Map<TaskBoard>(models);
            await _TaskBoardRepository.UpdateEntity(TaskBoard);
            return Ok();

        }
        /// <summary>
        /// Get TaskBoard by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion
    }
}
