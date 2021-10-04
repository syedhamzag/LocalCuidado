using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
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
    public class StaffInfectionControlController : ControllerBase
    {
        private IGenericRepository<StaffInfectionControl> _infectionRepository;


        public StaffInfectionControlController(IGenericRepository<StaffInfectionControl> infectionRepository)
        {
            _infectionRepository = infectionRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffInfectionControl>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _infectionRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffInfectionControl postCarePlanHygiene)
        {
            if (postCarePlanHygiene == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHygiene = Mapper.Map<StaffInfectionControl>(postCarePlanHygiene);
            await _infectionRepository.InsertEntity(CarePlanHygiene);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffInfectionControl models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHygiene = Mapper.Map<StaffInfectionControl>(models);
            await _infectionRepository.UpdateEntity(CarePlanHygiene);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffInfectionControl), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHygiene = await (from c in _infectionRepository.Table
                                            where c.InfectionId == id.Value
                                            select new GetStaffInfectionControl
                                            {
                                                StaffPersonalInfoId = c.StaffPersonalInfoId,
                                                Status = c.Status,
                                                InfectionId = c.InfectionId,
                                                Guideline = c.Guideline,
                                                VaccStatus = c.VaccStatus,
                                                Type = c.Type,
                                                TestDate = c.TestDate,
                                                Remarks = c.Remarks
                                                
                                            }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHygiene);
        }
    }
}
