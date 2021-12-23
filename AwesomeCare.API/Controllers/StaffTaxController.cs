using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax;
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
    public class StaffTaxController : ControllerBase
    {
        private IGenericRepository<StaffTax> _StaffTaxRepository;
        public StaffTaxController(IGenericRepository<StaffTax> StaffTaxRepository)
        {
            _StaffTaxRepository = StaffTaxRepository;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffTax), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffTaxRepository.Table
                                   where h.StaffTaxId == id && !h.Deleted
                                   select new GetStaffTax
                                   {
                                       Deleted = h.Deleted,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       StaffTaxId = h.StaffTaxId,
                                       Tax = h.Tax,
                                       NI = h.NI,
                                       Remarks = h.Remarks,


                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffTax>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffTaxRepository.Table.Where(c => !c.Deleted).ProjectTo<GetStaffTax>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffTax model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffTax>(model);
            await _StaffTaxRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffTax model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffTax>(model);
            await _StaffTaxRepository.UpdateEntity(postEntity);
            return Ok();
        }
    }
}
