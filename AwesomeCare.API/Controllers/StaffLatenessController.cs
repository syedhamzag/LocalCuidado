using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.Lateness;
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
    public class StaffLatenessController : ControllerBase
    {
        private IGenericRepository<StaffLateness> _StaffLatenessRepository;
        public StaffLatenessController(IGenericRepository<StaffLateness> StaffLatenessRepository)
        {
            _StaffLatenessRepository = StaffLatenessRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffLateness), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffLatenessRepository.Table
                                   where h.StaffLatenessId == id && !h.Deleted
                                   select new GetStaffLateness
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       StaffLatenessId = h.StaffLatenessId,
                                       SN = h.SN,
                                       Date = h.Date,
                                       Reason = h.Reason,
                                       Response = h.Response,
                                       Rota = h.Rota,
                                       Status = h.Status,
                                       TimeCritical = h.TimeCritical
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffLateness>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffLatenessRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffLateness>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostStaffLateness model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffLateness>(model);
            await _StaffLatenessRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffLateness model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffLateness>(model);
            await _StaffLatenessRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}
