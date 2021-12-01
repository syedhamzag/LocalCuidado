using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
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
    public class StaffHolidayController : ControllerBase
    {
        private IGenericRepository<StaffHoliday> _StaffHolidayRepository;
        public StaffHolidayController(IGenericRepository<StaffHoliday> StaffHolidayRepository)
        {
            _StaffHolidayRepository = StaffHolidayRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffHoliday), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffHolidayRepository.Table
                                   where h.StaffHolidayId == id && !h.Deleted
                                   select new GetStaffHoliday
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       StaffHolidayId = h.StaffHolidayId,
                                       AllocatedDays = h.AllocatedDays,
                                       Class = h.Class,
                                       CopyOfHandover = h.CopyOfHandover,
                                       Days = h.Days,
                                       EndDate = h.EndDate,
                                       PersonOnResponsibility = h.PersonOnResponsibility,
                                       Purpose = h.Purpose,
                                       Remark = h.Remark,
                                       StartDate = h.StartDate,
                                       YearOfService = h.YearOfService
   
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffHoliday>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffHolidayRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffHoliday>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffHoliday>(model);
            await _StaffHolidayRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffHoliday>(model);
            await _StaffHolidayRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}
