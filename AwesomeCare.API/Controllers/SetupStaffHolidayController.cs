using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.SetupStaffHoliday;
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
    public class SetupStaffHolidayController : ControllerBase
    {
        private IGenericRepository<SetupStaffHoliday> _SetupStaffHolidayRepository;
        public SetupStaffHolidayController(IGenericRepository<SetupStaffHoliday> SetupStaffHolidayRepository)
        {
            _SetupStaffHolidayRepository = SetupStaffHolidayRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetSetupStaffHoliday), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _SetupStaffHolidayRepository.Table
                                   where h.SetupHolidayId == id && !h.Deleted
                                   select new GetSetupStaffHoliday
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       Attachment = h.Attachment,
                                       HoursSoFar = h.HoursSoFar,
                                       IncrementalDailyHolidayByHrs = h.IncrementalDailyHolidayByHrs,
                                       NumberOfDays = h.NumberOfDays,
                                       SetupHolidayId = h.SetupHolidayId,
                                       TypeOfHoliday = h.TypeOfHoliday,
                                       YearEndPeriodStartDate = h.YearEndPeriodStartDate,
                                       YearOfEmployment = h.YearOfEmployment,
                                       Remark = h.Remark                             
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetSetupStaffHoliday>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _SetupStaffHolidayRepository.Table.Where(c => !c.Deleted).ProjectTo<GetSetupStaffHoliday>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostSetupStaffHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SetupStaffHoliday>(model);
            await _SetupStaffHolidayRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutSetupStaffHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SetupStaffHoliday>(model);
            await _SetupStaffHolidayRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}
