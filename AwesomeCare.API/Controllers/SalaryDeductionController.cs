using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryDeduction;
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
    public class SalaryDeductionController : ControllerBase
    {
        private IGenericRepository<SalaryDeduction> _SalaryDeductionRepository;
        public SalaryDeductionController(IGenericRepository<SalaryDeduction> SalaryDeductionRepository)
        {
            _SalaryDeductionRepository = SalaryDeductionRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetSalaryDeduction), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _SalaryDeductionRepository.Table
                                   where h.SalaryDeductionId == id && !h.Deleted
                                   select new GetSalaryDeduction
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       SalaryDeductionId = h.SalaryDeductionId,
                                       Reoccurent = h.Reoccurent,
                                       Amount = h.Amount,
                                       Percentage = h.Percentage,
                                       StartDate = h.StartDate,
                                       EndDate = h.EndDate,

                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetSalaryDeduction>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _SalaryDeductionRepository.Table.Where(c => !c.Deleted).ProjectTo<GetSalaryDeduction>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostSalaryDeduction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SalaryDeduction>(model);
            await _SalaryDeductionRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutSalaryDeduction model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<SalaryDeduction>(model);
            await _SalaryDeductionRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}

