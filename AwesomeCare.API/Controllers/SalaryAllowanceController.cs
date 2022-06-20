using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryAllowance;
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
    public class SalaryAllowanceController : ControllerBase
    {
        private IGenericRepository<SalaryAllowance> _SalaryAllowanceRepository;
        public SalaryAllowanceController(IGenericRepository<SalaryAllowance> SalaryAllowanceRepository)
        {
            _SalaryAllowanceRepository = SalaryAllowanceRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetSalaryAllowance), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _SalaryAllowanceRepository.Table
                                   where h.SalaryAllowanceId == id && !h.Deleted
                                   select new GetSalaryAllowance
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       SalaryAllowanceId = h.SalaryAllowanceId,
                                       Reoccurent = h.Reoccurent,
                                       Amount = h.Amount,
                                       Percentage = h.Percentage,
                                       StartDate = h.StartDate,
                                       EndDate = h.EndDate,

                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetSalaryAllowance>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _SalaryAllowanceRepository.Table.Where(c => !c.Deleted).ProjectTo<GetSalaryAllowance>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostSalaryAllowance model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SalaryAllowance>(model);
            await _SalaryAllowanceRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutSalaryAllowance model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SalaryAllowance>(model);
            await _SalaryAllowanceRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}
