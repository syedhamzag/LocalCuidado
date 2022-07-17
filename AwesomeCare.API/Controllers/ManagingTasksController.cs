using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
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
    public class ManagingTasksController : ControllerBase
    {
        private IGenericRepository<ManagingTasks> _taskRepository;


        public ManagingTasksController(IGenericRepository<ManagingTasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #region CarePlanHygiene
        /// <summary>
        /// Get All CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetManagingTasks>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _taskRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHygiene
        /// </summary>
        /// <param name="postCarePlanHygiene"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostManagingTasks> postCarePlanHygiene)
        {
            if (postCarePlanHygiene == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHygiene = Mapper.Map<List<ManagingTasks>>(postCarePlanHygiene);
            await _taskRepository.InsertEntities(CarePlanHygiene);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutManagingTasks> puttask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHygiene = Mapper.Map<List<ManagingTasks>>(puttask);
            foreach (var item in CarePlanHygiene)
            {
                await _taskRepository.UpdateEntity(item);
            }           
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHygiene by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetManagingTasks), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHygiene = await (from c in _taskRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetManagingTasks
                                           {
                                               TaskId = c.TaskId,
                                               Help = c.Help,
                                               ClientId = c.ClientId,
                                               Task = c.Task,
                                               Status = c.Status

                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHygiene);
        }
        /// <summary>
        /// Get CarePlanHygiene by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetManagingTasks), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHygiene = await (from c in _taskRepository.Table
                                            where c.ClientId == id.Value
                                            select new GetManagingTasks
                                            {
                                                TaskId = c.TaskId,
                                                Help = c.Help,
                                                ClientId = c.ClientId,
                                                Task = c.Task,
                                                Status = c.Status

                                            }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHygiene);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _taskRepository.GetEntity(id);
            await _taskRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
